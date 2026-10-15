using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

namespace Effects
{
    public class ScreenEffectFeature : ScriptableRendererFeature
    {
        [SerializeField] RenderPassEvent passEvent = RenderPassEvent.AfterRenderingPostProcessing;

        ScreenEffectPass _pass;

        public override void Create()
        {
            _pass = new ScreenEffectPass
            {
                renderPassEvent = passEvent,
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (_pass == null)
                return;

            CameraType type = renderingData.cameraData.cameraType;
            if (type != CameraType.Game && type != CameraType.SceneView)
                return;

            _pass.renderPassEvent = passEvent;
            renderer.EnqueuePass(_pass);
        }

        class ScreenEffectPass : ScriptableRenderPass
        {
            static bool s_WarnedBackBuffer;
            readonly List<Material> _materials = new List<Material>();

            public ScreenEffectPass()
            {
                requiresIntermediateTexture = true;
            }

            public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
            {
                UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
                Camera camera = cameraData.camera;
                if (camera == null)
                    return;

                ScreenEffectPlayer player = camera.GetComponent<ScreenEffectPlayer>();
                if (player == null)
                    return;

                player.CopyBlitMaterials(_materials);
                if (_materials.Count == 0)
                    return;

                UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
                if (resourceData.isActiveTargetBackBuffer)
                {
                    if (!s_WarnedBackBuffer)
                    {
                        s_WarnedBackBuffer = true;
                        Debug.LogWarning("ScreenEffect: 当前相机画到了 BackBuffer，全屏效果被跳过。");
                    }
                    return;
                }

                TextureHandle current = resourceData.activeColorTexture;
                bool wrote = false;
                for (int i = 0; i < _materials.Count; i++)
                {
                    Material material = _materials[i];
                    if (material == null)
                        continue;

                    var description = renderGraph.GetTextureDesc(current);
                    description.name = "ScreenEffect";
                    description.clearBuffer = false;
                    description.depthBufferBits = 0;
                    TextureHandle destination = renderGraph.CreateTexture(description);

                    var blit = new RenderGraphUtils.BlitMaterialParameters(current, destination, material, 0);
                    renderGraph.AddBlitPass(blit, "Screen Effect");
                    current = destination;
                    wrote = true;
                }

                if (wrote)
                    resourceData.cameraColor = current;
            }
        }
    }

}

