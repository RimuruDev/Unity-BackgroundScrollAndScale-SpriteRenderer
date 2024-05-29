// Resharper disable all
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
// **************************************************************** //

using UnityEngine;

namespace RimuruDev
{
    using UnityEngine;

    namespace RimuruDev
    {
        [SelectionBase]
        [DisallowMultipleComponent]
        [RequireComponent(typeof(SpriteRenderer))]
        public sealed class BackgroundScrollAndScale : MonoBehaviour
        {
            [SerializeField] private Camera cameraRenderer;
            [SerializeField] private Vector2 scrollSpeed;
            private SpriteRenderer spriteRenderer;
            private Material material;

            private void Awake()
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                material = spriteRenderer.material;
            }

            private void Start() =>
                ScaleBackground();

            private void LateUpdate()
            {
                ScaleBackground();
                ScrollBackground(Time.deltaTime);
            }

            private void ScaleBackground()
            {
                var targetHeight = cameraRenderer.orthographicSize * 2;
                var targetWidth = targetHeight * Screen.width / Screen.height;

                var backgroundSize = spriteRenderer.sprite.bounds.size;

                var widthRatio = targetWidth / backgroundSize.x;
                var heightRatio = targetHeight / backgroundSize.y;

                var targetScale = Vector3.one;

                if (widthRatio > heightRatio)
                    targetScale *= widthRatio;
                else
                    targetScale *= heightRatio;

                transform.localScale = targetScale;
            }

            private void ScrollBackground(float delta)
            {
                if (Application.isPlaying)
                {
                    material.SetFloat("_ScrollX", scrollSpeed.x);
                    material.SetFloat("_ScrollY", scrollSpeed.y);
                    material.SetFloat("_TimeY", Time.time);
                }
                else
                {
                    material.SetFloat("_ScrollX", 0);
                    material.SetFloat("_ScrollY", 0);
                    material.SetFloat("_TimeY", 0);
                }
            }
        }
    }
}
