using UnityEngine;
using System.Collections;

public class IgnoreCollision : MonoBehaviour
{
    private void Start()
    {
        CharacterController controller;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            controller.detectCollisions = false;
        }

    }

}