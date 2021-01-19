using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRM
{
    public class uLipSyncVRMBlendShape : MonoBehaviour
    {
        VRMBlendShapeProxy _vrmBlendShapeProxy;

        float volume_ = 0f;
        Dictionary<BlendShapePreset, float> bsValue = new Dictionary<BlendShapePreset, float>()
        {
            {BlendShapePreset.A, 0.0f },
            {BlendShapePreset.I, 0.0f },
            {BlendShapePreset.U, 0.0f },
            {BlendShapePreset.E, 0.0f },
            {BlendShapePreset.O, 0.0f },
            {BlendShapePreset.Neutral, 0.0f }
        };

        public void OnLipSyncUpdate(uLipSync.LipSyncInfo info)
        {
            if(_vrmBlendShapeProxy == null) { return; }
            foreach (var kv in info.vowels)
            {
                switch(kv.Key)
                {
                    case uLipSync.Vowel.A:
                        bsValue[BlendShapePreset.A] = kv.Value;
                        break;
                    case uLipSync.Vowel.I:
                        bsValue[BlendShapePreset.I] = kv.Value;
                        break;
                    case uLipSync.Vowel.U:
                        bsValue[BlendShapePreset.U] = kv.Value;
                        break;
                    case uLipSync.Vowel.E:
                        bsValue[BlendShapePreset.E] = kv.Value;
                        break;
                    case uLipSync.Vowel.O:
                        bsValue[BlendShapePreset.O] = kv.Value;
                        break;
                    case uLipSync.Vowel.None:
                        bsValue[BlendShapePreset.Neutral] = kv.Value;
                        break;
                    default:
                        break;
                }
            }
            volume_ = info.volume;
        }


        public void OnVRMBSP(VRMBlendShapeProxy inBSP)
        {
            _vrmBlendShapeProxy = inBSP;
        }

        private void Start()
        {
            var vBSP = GetComponent<VRMBlendShapeProxy>();
            if(vBSP != null){
                _vrmBlendShapeProxy = vBSP;
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (_vrmBlendShapeProxy == null) { return; }
            _vrmBlendShapeProxy.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Neutral), bsValue[BlendShapePreset.Neutral]);
            _vrmBlendShapeProxy.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), bsValue[BlendShapePreset.A] * volume_);
            _vrmBlendShapeProxy.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.I), bsValue[BlendShapePreset.I] * volume_);
            _vrmBlendShapeProxy.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.U), bsValue[BlendShapePreset.U] * volume_);
            _vrmBlendShapeProxy.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.E), bsValue[BlendShapePreset.E] * volume_);
            _vrmBlendShapeProxy.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), bsValue[BlendShapePreset.O] * volume_);
        }
    }
}