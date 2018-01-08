var cameraToLookAt : Camera;

function Update() {
	var v : Vector3 = cameraToLookAt.transform.position - transform.position;
	v.x = v.z = 0.0;
	transform.LookAt(cameraToLookAt.transform.position - v); 
}