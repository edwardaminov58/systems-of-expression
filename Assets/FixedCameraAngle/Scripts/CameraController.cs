using UnityEngine;

namespace KreliStudio
{
    public class CameraController : MonoBehaviour
    {
        #region Variables
        private CameraSocket cameraScocket;
        private bool isSmoothPath = false;
        private bool isMotion = false;
        private bool isLookAt = false;
        #endregion

        #region Main Methods
        public void SetNewCameraSocket(CameraSocket _cameraSocket)
        {
            // old camera socket set to deactive
            if (cameraScocket)
                cameraScocket.SetActive(false);

            cameraScocket = _cameraSocket;
            cameraScocket.SetActive(true);
            // set this properties to camera for more optimision because it should not change in while
            // and it will be use in FixedUpdate method
            isMotion = (cameraScocket.socketType == SocketType.MOTION) ? true : false;
            isLookAt = cameraScocket.lookAtTarget;
            isSmoothPath = cameraScocket.smoothPath;

            // if is not motion or of is only one path node
            if (!isMotion || cameraScocket.cameraNode.Count == 1) { 
                transform.position = cameraScocket.cameraNode[0].Position;
                transform.rotation = cameraScocket.cameraNode[0].rotation;
            }
            // if is motion and not smooth transition between two camera sockets then jump to new socket position
            else if (isMotion && !cameraScocket.smoothTransition)
            {
                transform.position = cameraScocket.MotionPosition();
                if (!isLookAt)
                    transform.rotation = cameraScocket.Orientation();
                else
                    transform.LookAt(cameraScocket.target);

            }
        }
        #endregion

        #region Behaviour Methods
        private void FixedUpdate()
        {
            if (isMotion)
            {
                if (isSmoothPath)
                {
                    transform.position = Vector3.Lerp(transform.position, cameraScocket.MotionPosition(), Time.deltaTime * cameraScocket.smoothness);
                    if (!isLookAt)
                        transform.rotation = Quaternion.Lerp(transform.rotation, cameraScocket.Orientation(), Time.deltaTime * cameraScocket.smoothness);
                }
                else
                {
                    transform.position = cameraScocket.MotionPosition();
                    if (!isLookAt)
                        transform.rotation = cameraScocket.Orientation();
                }
            }

            if (isLookAt)
                transform.LookAt(cameraScocket.target);
        }
        #endregion
    }
}
