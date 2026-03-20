using UnityEngine;

[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer blWheelMesh;
    public MeshRenderer brWheelMesh;
    public MeshRenderer flWheelMesh;
    public MeshRenderer frWheelMesh;
}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider blWheelCollider;
    public WheelCollider brWheelCollider;
    public WheelCollider flWheelCollider;
    public WheelCollider frWheelCollider;
}
public class Vehicle : MonoBehaviour
{
    public WheelMeshes wheelMeshes;
    public WheelColliders wheelColliders;

    private void FixedUpdate()
    {
        ApplyWheelRotation();
    }

    void ApplyWheelRotation()
    {
        UpdateWheel(wheelColliders.blWheelCollider, wheelMeshes.blWheelMesh);
        UpdateWheel(wheelColliders.brWheelCollider, wheelMeshes.brWheelMesh);
        UpdateWheel(wheelColliders.flWheelCollider, wheelMeshes.flWheelMesh);
        UpdateWheel(wheelColliders.frWheelCollider, wheelMeshes.frWheelMesh);
    }

    void UpdateWheel(WheelCollider col, MeshRenderer mesh)
    {
        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);
        mesh.transform.position = pos;
        mesh.transform.rotation = rot;
    }
}



