using System;
using System.Collections.Generic;
using System.Text;
using TeamCherry.SharedUtils;
using UnityEngine;

namespace CinematicTitleScreen.Modules
{
    internal class PositionToCamera : MonoBehaviour
    {
        tk2dCamera camera;
        Vector3 Offset = new Vector3(0, 0.8f, 12);
        void Awake()
        {
            transform.SetScaleMatching(0.2f);

            var system = gameObject.AddComponent<ParticleSystem>();
            var main = system.main;
            main.loop = true;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.startSpeed = 0;

            var emission = system.emission;
            emission.rateOverTime = 0;
            emission.rateOverDistance = 5;

            var shape = system.shape;
            shape.scale = new Vector3(0.001f, 0.001f, 1);
        }

        void LateUpdate()
        {
            if (!camera)
            {
                camera = GameCameras.instance.tk2dCam;
            }

            var newPos = camera.transform.position + Offset;

            transform.position = newPos;
        }
    }
}
