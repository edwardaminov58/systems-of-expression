using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KreliStudio
{
    [RequireComponent(typeof(Collider))]
    public class CameraSocket : MonoBehaviour
    {
        #region Variables
        public SocketType socketType = SocketType.STATIC; // type of camera control static or motion
        public PathMode pathMode = PathMode.LINEAR; // type of calculate path linear is point to point, curve is more smooth and round
        public bool lookAtTarget = false; // if true camera allways look at target
        public Transform target; // player or something what you want to follow
        public List<Node> cameraNode = new List<Node>(); // nodes contain camera motion path (position and rotation)
        public List<Node> followPath = new List<Node>(); // nodes contain target path related proportionally to camera nodes
        public List<Collider> colliders = new List<Collider>(); // triggers starting the camera socket 
        public bool smoothPath = false; // if true the camera does not jump between far points but move smoothly beetwen this points inside path
        public bool smoothTransition = false; // if true the camera does not jump between new camera socket but move smoothly beetwen this current position to new camera socket
        public float smoothness = 1f; // smooth path scalar
        
        private int segment = 0; // segment is number of line between two camera nodes (only get because its calculate by socket)
        public int Segment
        {
            get
            {
                return segment;
            }
        }
        private float ratio = 0; // ratio is a percentage of distance between nodes and target (only get because its calculate by socket)
        public float Ratio
        {
            get
            {
                return ratio;
            }
        }
        private bool isActive = false; // if target is inside collider (only get)
        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        public bool gizmosIcon = true;
        public bool gizmosFrustum = true;
        public bool gizmosMotion = true;
        public bool gizmosCollider = true;
        public bool gizmosHandles = true;
        public Color gizmosColliderColor = new Color(0.3f, 1f, 0f, 1f);
        public Color gizmosPathColor = new Color(0.15f, 0.65f, 0.9f, 1f);
        public int gizmosCurveScale = 10;
        public int subMenu = 0; // variable for custom editor to remember open menu 

        private Vector3 closestSegmentPoint = Vector3.positiveInfinity; // helper value for calculate nearest point between follow nodes
        #endregion

        #region Main Methods
        public void AddCollider(int _colliderType)
        {
            Collider _collider;
            switch (_colliderType)
            {
                default:
                case 0:
                    BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
                    boxCollider.center = Vector3.right;
                    _collider = boxCollider;
                    break;
                case 1:
                    SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
                    sphereCollider.center = Vector3.right;
                    _collider = sphereCollider;
                    break;
            }
            _collider.isTrigger = true;
            colliders.Add(_collider);
        }
        public void RemoveCollider(int _id)
        {
            if (colliders[_id])
                DestroyImmediate(colliders[_id]);
            colliders.RemoveAt(_id);
        }
        public void AddSocket()
        {
            Vector3 previouseSocket = Vector3.zero;
            Vector3 previouseFollow = Vector3.zero;
            if (cameraNode.Count > 0)
            {
                previouseSocket = cameraNode[cameraNode.Count - 1].Position + Vector3.right - transform.position;
                previouseFollow = followPath[followPath.Count - 1].Position + Vector3.right - transform.position;
            }
            cameraNode.Add(new Node(this, previouseSocket, Quaternion.identity));
            followPath.Add(new Node(this, previouseFollow, Quaternion.identity));
        }
        public void RemoveSocket(int _id)
        {
            cameraNode.RemoveAt(_id);
            followPath.RemoveAt(_id);
        }

        public void SetActive(bool _isActive)
        {
            isActive = _isActive;
        }
        // Transform camera position according segment & ratio
        public Vector3 MotionPosition()
        {
            switch (pathMode)
            {
                default:
                case PathMode.LINEAR:
                    return LinearPosition(segment, ratio);
                case PathMode.CURVE:
                    return CurvePosition(segment, ratio);
            }
        }
        // Transform camera rotation according segment & ratio
        public Quaternion Orientation()
        {
            Quaternion rot1, rot2;
            rot1 = cameraNode[segment].rotation;
            if (segment < cameraNode.Count - 1)
            {
                rot2 = cameraNode[segment + 1].rotation;
            }
            else
            {
                rot2 = cameraNode[segment].rotation;
            }
            return Quaternion.Lerp(rot1, rot2, ratio);
        }
        #endregion

        #region Behaviour Methods
        // if target enter the trigger cameraController start useing this socket
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == target)
            {
                Camera.main.GetComponent<CameraController>().SetNewCameraSocket(this);
            }
        }
        // if target leave this trigget cameraController stop using this socket
        private void OnTriggerExit(Collider other)
        {
            if (other.transform == target)
            {
                bool isTargetInBounds = false;
                // if target exit one collder check if it is still inside of some colliders 
                foreach (Collider c in colliders)
                {
                    if (c.bounds.Contains(target.transform.position)){
                        isTargetInBounds = true;
                    }
                }
                // if its out of all socket's colliders then deactive socket
                if (!isTargetInBounds)
                {
                    isActive = false;
                }
            }
        }

        private void Update()
        {
            CalculateSegmentAndRatio();
        }
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Draw Icon
            Gizmos.DrawIcon(transform.position, "Transform");
            if (gizmosIcon)
            {
                string iconName = "CameraSocket";
                if (lookAtTarget)
                    iconName += "_LookAt";
                if (socketType == SocketType.MOTION)
                    iconName += "_Move";
                Gizmos.DrawIcon(cameraNode[0].Position, iconName);
            }
            // Draw Frustum
            if (gizmosFrustum)
            {
                Matrix4x4 temp = Gizmos.matrix;
                Gizmos.color = new Color(gizmosPathColor.r, gizmosPathColor.g, gizmosPathColor.b, 0.5f);
                Gizmos.matrix = Matrix4x4.TRS(cameraNode[0].Position, cameraNode[0].rotation, Vector3.one);
                Gizmos.DrawFrustum(Vector3.zero, 60.0f, 2.5f, 0.25f, 1);
                Gizmos.matrix = temp;
                Gizmos.color = Color.white;
            }
            // Draw Collider
            if (gizmosCollider)
            {
                Matrix4x4 temp = Gizmos.matrix;
                foreach(Collider c in GetComponents<Collider>())
                {
                    Gizmos.color = new Color(gizmosColliderColor.r, gizmosColliderColor.g, gizmosColliderColor.b, 0.15f);
                    Gizmos.matrix = Matrix4x4.TRS(c.bounds.center, transform.rotation, c.bounds.size);

                    if (c.GetType() == typeof(SphereCollider)) { // draw sphere collider gizmos
                        Gizmos.DrawSphere(Vector3.zero, 0.5f);
                        Gizmos.color = new Color(gizmosColliderColor.r, gizmosColliderColor.g, gizmosColliderColor.b, 0.9f);
                        Gizmos.DrawWireSphere(Vector3.zero, 0.5f);
                    }
                    else                                        // draw box collider gizmos
                    {
                        Gizmos.DrawCube(Vector3.zero, Vector3.one);
                        Gizmos.color = new Color(gizmosColliderColor.r, gizmosColliderColor.g, gizmosColliderColor.b, 0.9f);
                        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                    }

                }
                Gizmos.matrix = temp;
                Gizmos.color = Color.white;

            }
            // Draw Motion Path
            if (socketType == SocketType.MOTION)
            {
                // draw first follow node 
                if (gizmosIcon)
                {
                    Gizmos.DrawIcon(followPath[0].Position, "FollowNode");
                }
                if (gizmosMotion)
                {
                    // line between first follow node and first camera node
                    Handles.color = new Color(gizmosPathColor.r, gizmosPathColor.g, gizmosPathColor.b, 0.5f);
                    Handles.DrawDottedLine(cameraNode[0].Position, followPath[0].Position, 10.0f);
                    // draw line betwen segment point and target
                    if (isActive)
                    {
                        // line between closet point on follow path and target
                        Handles.color = new Color(gizmosPathColor.r, gizmosPathColor.g, gizmosPathColor.b, 0.75f);
                        Handles.DrawDottedLine(target.position, closestSegmentPoint, 2.0f);
                        Gizmos.color = new Color(gizmosPathColor.r, gizmosPathColor.g, gizmosPathColor.b, 0.75f);
                        Gizmos.DrawWireSphere(closestSegmentPoint, 0.1f);
                    }
                    Handles.color = Color.white;
                }
                for (int i = 1; i < cameraNode.Count; i++)
                {
                    if (gizmosIcon)
                    {
                        Gizmos.DrawIcon(cameraNode[i].Position, "MotionNode");
                        Gizmos.DrawIcon(followPath[i].Position, "FollowNode");
                    }

                    if (gizmosMotion)
                    {
                        Gizmos.color = new Color(gizmosPathColor.r, gizmosPathColor.g, gizmosPathColor.b, 0.65f);
                        // line between previous camera node and next camera node
                        if (pathMode == PathMode.LINEAR)
                        {
                            Gizmos.DrawLine(cameraNode[i - 1].Position, cameraNode[i].Position);
                        }
                        else
                        {
                            List<Vector3> curvePosition = new List<Vector3>();
                            Handles.color = new Color(gizmosPathColor.r, gizmosPathColor.g, gizmosPathColor.b, 0.65f);
                            for (int j = 0; j <= gizmosCurveScale; j++)
                            {
                                curvePosition.Add(CurvePosition(i-1, j * 1.0f / gizmosCurveScale));
                            }
                            Handles.DrawPolyLine(curvePosition.ToArray());
                        }
                        // line between previous follow node and next follow node
                        Gizmos.DrawLine(followPath[i - 1].Position, followPath[i].Position);
                        // line between follow node and camera node
                        Handles.color = new Color(gizmosPathColor.r, gizmosPathColor.g, gizmosPathColor.b, 0.65f);
                        Handles.DrawDottedLine(cameraNode[i].Position, followPath[i].Position, 10.0f);
                        Handles.color = Color.white;                        
                    }
                    // draw camera node frustum
                    if (gizmosFrustum)
                    {
                        Matrix4x4 temp = Gizmos.matrix;
                        Gizmos.matrix = Matrix4x4.TRS(cameraNode[i].Position, cameraNode[i].rotation, Vector3.one);
                        Gizmos.color = new Color(gizmosPathColor.r, gizmosPathColor.g, gizmosPathColor.b, 0.5f);
                        Gizmos.DrawFrustum(Vector3.zero, 60.0f, 1.0f, 0.1f, 1);
                        Gizmos.matrix = temp;
                        Gizmos.color = Color.white;
                    }
                }
            }
            
        }
        #endif
        #endregion

        #region Helper Methods
        // move camera from point to point lineary
        private Vector3 LinearPosition(int _segment,float _ratio)
        {
            Vector3 pos1, pos2;
            pos1 = cameraNode[_segment].Position;
            if (_segment < cameraNode.Count - 1)
            {
                pos2 = cameraNode[_segment + 1].Position;
            }
            else
            {
                pos2 = cameraNode[_segment].Position;
            }
            return Vector3.Lerp(pos1, pos2, _ratio);
        }
        // move camera from point to point more smoothly and roundly
        private Vector3 CurvePosition(int _segment, float _ratio)
        {
            if (cameraNode.Count > 1)
            {
                Vector3 pos1, pos2, pos3, pos4;

                if (cameraNode.Count == 2) // if is two nodes and one segment between
                {
                    if (_segment == 0)
                    {
                        pos1 = cameraNode[_segment].Position;
                        pos2 = cameraNode[_segment].Position;
                        pos3 = cameraNode[_segment + 1].Position;
                        pos4 = cameraNode[_segment + 1].Position;
                    }
                    else
                    {
                        pos1 = cameraNode[_segment - 1].Position;
                        pos2 = cameraNode[_segment].Position;
                        pos3 = cameraNode[_segment].Position;
                        pos4 = cameraNode[_segment].Position;
                    }
                }
                else // if is greater than 1 segments in path
                {
                    if (_segment == 0)
                    {
                        pos1 = cameraNode[_segment].Position;
                        pos2 = cameraNode[_segment].Position;
                        pos3 = cameraNode[_segment + 1].Position;
                        pos4 = cameraNode[_segment + 2].Position;
                    }
                    else if (_segment == cameraNode.Count - 2)
                    {
                        pos1 = cameraNode[_segment - 1].Position;
                        pos2 = cameraNode[_segment].Position;
                        pos3 = cameraNode[_segment + 1].Position;
                        pos4 = cameraNode[_segment + 1].Position;
                    }
                    else if (_segment == cameraNode.Count - 1)
                    {
                        pos1 = cameraNode[_segment - 1].Position;
                        pos2 = cameraNode[_segment].Position;
                        pos3 = cameraNode[_segment].Position;
                        pos4 = cameraNode[_segment].Position;
                    }
                    else
                    {
                        pos1 = cameraNode[_segment - 1].Position;
                        pos2 = cameraNode[_segment].Position;
                        pos3 = cameraNode[_segment + 1].Position;
                        pos4 = cameraNode[_segment + 2].Position;
                    }
                }

                // super complicated mathematical operations
                float x = 0.5f * ((2.0f * pos2.x) +
                    (-pos1.x + pos3.x) * _ratio +
                    (2.0f * pos1.x - 5.0f * pos2.x + 4.0f * pos3.x - pos4.x) * _ratio * _ratio +
                    (-pos1.x + 3.0f * pos2.x - 3.0f * pos3.x + pos4.x) * _ratio * _ratio * _ratio);

                float y = 0.5f * ((2.0f * pos2.y) +
                    (-pos1.y + pos3.y) * _ratio +
                    (2.0f * pos1.y - 5.0f * pos2.y + 4.0f * pos3.y - pos4.y) * _ratio * _ratio +
                    (-pos1.y + 3.0f * pos2.y - 3.0f * pos3.y + pos4.y) * _ratio * _ratio * _ratio);

                float z = 0.5f * ((2.0f * pos2.z) +
                    (-pos1.z + pos3.z) * _ratio +
                    (2.0f * pos1.z - 5.0f * pos2.z + 4.0f * pos3.z - pos4.z) * _ratio * _ratio +
                    (-pos1.z + 3.0f * pos2.z - 3.0f * pos3.z + pos4.z) * _ratio * _ratio * _ratio);

                return new Vector3(x, y, z);
            }
            else // if is one 
                return cameraNode[0].Position;

        }
        // calculate nearest point at followPath from target
        private void CalculateSegmentAndRatio()
        {
            // is target is inside socket area (collider) and type is motion and is more camera nodes than one then
            if (isActive && socketType == SocketType.MOTION && cameraNode.Count > 1)
            {
                // check all segments (line between two nodes) from follow path
                for (int i = 1; i < followPath.Count; i++)
                {
                    // get nearest point on segment between i-1 and i nodes
                    Vector3 segmentPoint = ClosestPointToLine(followPath[i - 1].Position, followPath[i].Position, target.position);
                    // get distance from target to segment point
                    float distanceToPoint = Vector3.Distance(target.position, segmentPoint);
                    // get distance from target to saved nearest segment point
                    float distanceToClosestPoint = Vector3.Distance(target.position, closestSegmentPoint);
                    // if new segment point is closest than saved one then 
                    if (distanceToPoint < distanceToClosestPoint)
                    {
                        // set new segment point for the next compare
                        closestSegmentPoint = segmentPoint;
                        // set actual nerest segment
                        segment = i - 1;
                    }
                }
                // calculate ratio  by get distance from i-1 node and nerest segment point and get distance whole segment from node i-1 and i then division it
                ratio = Vector3.Distance(followPath[segment].Position, closestSegmentPoint) / Vector3.Distance(followPath[segment].Position, followPath[segment + 1].Position);
            }
        }
        // calculate nearest point on line
        private Vector3 ClosestPointToLine(Vector3 _lineStartPoint, Vector3 _lineEndPoint, Vector3 _testPoint)
        {
            Vector3 pointTowardStart = _testPoint - _lineStartPoint;
            Vector3 startTowardEnd = (_lineEndPoint - _lineStartPoint).normalized;

            float lengthOfLine = Vector3.Distance(_lineStartPoint, _lineEndPoint);
            float dotProduct = Vector3.Dot(startTowardEnd, pointTowardStart);

            if (dotProduct <= 0)
            {
                return _lineStartPoint;
            }

            if (dotProduct >= lengthOfLine)
            {
                return _lineEndPoint;
            }

            Vector3 thirdVector = startTowardEnd * dotProduct;

            Vector3 closestPointOnLine = _lineStartPoint + thirdVector;

            return closestPointOnLine;
        }
        #endregion
    }

    #region Helper Classes and Enums
    public enum SocketType
    {
        STATIC,
        MOTION
    }
    public enum PathMode
    {
        LINEAR,
        CURVE
    }
    [System.Serializable]
    public class Node
    {
        [SerializeField] private Vector3 position;
        public Quaternion rotation;
        public bool isFoldout; // variable for custom editor to foldout node
        [SerializeField] private CameraSocket cameraSocket; // parent camera socket

        public Vector3 Position
        {
            get
            {
                return position + cameraSocket.transform.position;
            }

            set
            {
                position = value - cameraSocket.transform.position;
            }
        }

        public Node(CameraSocket _cameraSocket, Vector3 _position, Quaternion _rotation)
        {
            cameraSocket = _cameraSocket;
            position = _position;
            rotation = _rotation;
        }
        public Node(CameraSocket _cameraSocket, Node _node)
        {
            cameraSocket = _cameraSocket;
            position = _node.position;
            rotation = _node.rotation;
        }
    }
    #endregion
}