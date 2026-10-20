#include "pch-cpp.hpp"





template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct GenericVirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_virtual_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1, typename T2, typename T3>
struct InterfaceFuncInvoker3
{
	typedef R (*Func)(void*, T1, T2, T3, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1, T2 p2, T3 p3)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, p3, invokeData.method);
	}
};
template <typename T1>
struct GenericInterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_interface_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

struct method_t81535E3A9A313D01BC63006B5A25610B21847FBD;
struct method_tF13E4C1D2723EFA21A0B8D9A21D42FE0334B238E;
struct AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct PlayableBindingU5BU5D_tC50C3F27A8E4246488F7A5998CAABAC4811A92CD;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263;
struct AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941;
struct AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20;
struct AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04;
struct AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311;
struct AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A;
struct AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2;
struct AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299;
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67;
struct Delegate_t;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E;
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct MethodInfo_t;
struct NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8;
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
struct ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct Type_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E;
struct PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072;
struct SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30;
struct AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060;
struct CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509;

IL2CPP_EXTERN_C RuntimeClass* ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral0574E31B2E15797E57C51DEC554AE14E00CCB9CC;
IL2CPP_EXTERN_C String_t* _stringLiteral0D4DFFEDA70FA6D865B68C20C1F40F10FC31B6DD;
IL2CPP_EXTERN_C String_t* _stringLiteral0FC2A6DFBAD242B7CE1A74ACA9D3224CE2E0163D;
IL2CPP_EXTERN_C String_t* _stringLiteral23ABA34B45DF51C7D9C0E116AA6FDA4749E8C618;
IL2CPP_EXTERN_C String_t* _stringLiteral49403A17E8D32B35CB5B66AB1A2651A7EEAD1B00;
IL2CPP_EXTERN_C String_t* _stringLiteral4EBC86E0EACFCA522AEB82874860D0E248D782A5;
IL2CPP_EXTERN_C String_t* _stringLiteral57186E11345D0E246E80500DE17B8C941EF8A38F;
IL2CPP_EXTERN_C String_t* _stringLiteral5A10D0C56FEA8FB8816E3E1F6F88D55578F844F7;
IL2CPP_EXTERN_C String_t* _stringLiteral5C2C12AF4EFFBF824B747CFFBFC316CAE24077D1;
IL2CPP_EXTERN_C String_t* _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5;
IL2CPP_EXTERN_C String_t* _stringLiteral706336A4F6F53F903F6BD416474754F528D2A13B;
IL2CPP_EXTERN_C String_t* _stringLiteralC51F74D9F7385459B7E90865166418A267D9A62B;
IL2CPP_EXTERN_C String_t* _stringLiteralC767BA736F2BEEB0EC1BA27ABE77214EC763C9B6;
IL2CPP_EXTERN_C String_t* _stringLiteralD5F2B3561CFA951FF37495C4B6AE6DB436EC2259;
IL2CPP_EXTERN_C String_t* _stringLiteralD7EA9435CDE6F5B13E32FE646D502752ECCD43DD;
IL2CPP_EXTERN_C String_t* _stringLiteralEE560D196E71D95F90A39910549B06428576F27C;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClipPlayable_SetSpatialBlend_m72F1B86E4E5940E8587C1EA32BB48463A00810BB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClipPlayable_SetStereoPan_mB8403E93B06B2ECCD0D4D6B0D55C151D0CCCC494_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClipPlayable_SetVolume_m3553EC43CBA43CA7802292710A1A284A6DEF8FCC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClipPlayable__ctor_m4E686B92CFA7C6A36AA794B1E8C36B1E60605751_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isFinite_m20E61329E86505BA44C480115CD9A403FC63308C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isRealtime_mAC78FD95FB9998B38D0E50C2462DC9D9147FEAA4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_length_mCA231C69B56E93689C2B45DCE2C3F0AD45CF541B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_UnityEngine_Audio_IAudioGenerator_CreateInstance_m78256DB80A6F6DB7AEF47DB8A3E827723D228FD5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioMixerPlayable__ctor_m2CDDE33FCE0B3F7D9EB482D76515FBF771285F47_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioPlayableBinding_CreateAudioOutput_m2BE0EDC74ADA082B5CD6247ACD78EFCBDB85ADC3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioPlayableOutput__ctor_mBEA3D7EE652908558720EDC0D40F7BF3EC50D409_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* GCHandle_get_Target_m481F9508DA5E384D33CD1F4450060DC56BBD4CD5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MarshalledUnityObject_Marshal_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m4CF5833467B7EAC854506E5E808202FABD1F15CC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MarshalledUnityObject_Marshal_TisAudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311_m1580BA03AE5DDE6E684BC285BEC86628223AA9C3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MarshalledUnityObject_Marshal_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m792F1F32E543E9AE922C6F04B2616F0690E1D1CB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MarshalledUnityObject_Marshal_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m965B12ED56AA1618C042D16D10B1BA3BFD98FF4D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* PlayableExtensions_SetDuration_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m67FC8BE5E310D54C91DB1D492F7393237401332E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* PlayableHandle_IsPlayableOfType_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m95809D16DD3494AEB0E9E652B42E0CE3167BEE0E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* PlayableHandle_IsPlayableOfType_TisAudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C_m28F89BE3D7CCE69B28258CC5B1C7D4E814F48CAB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* PlayableOutputHandle_IsPlayableOutputOfType_TisAudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20_mAC4BC6AF8D183B1A972EA239A6D9BEA34054C7C6_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Unmarshal_UnmarshalUnityObject_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m27ACA708E2D42A879C8C665663669217265002CF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* UnsafeUtility_As_TisIntPtr_t_TisGCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC_mD94F597CB91662C823496DA9FA9A7CF908734A78_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_0_0_0_var;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060;;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com;;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke;;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com;

struct AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t462BCCFB9B78348533823E0754F65F52A5348F89 
{
};
struct AudioPlayableBinding_tCEBDA2DEB832C1B7A2698EC9DAB066F7D6D85D3F  : public RuntimeObject
{
};
struct AudioPlayableGraphExtensions_tFE52BCEBCCE023697BAC3291D42A6659E973F50E  : public RuntimeObject
{
};
struct AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2  : public RuntimeObject
{
	SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* ___sampleFramesAvailable;
	SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* ___sampleFramesOverflow;
};
struct AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD  : public RuntimeObject
{
};
struct MemberInfo_t  : public RuntimeObject
{
};
struct ScriptableGeneratorBindings_tB9FD8D788D5EDF66E708E851437C25CA5ECA514D  : public RuntimeObject
{
};
struct ScriptableProcessorBindings_t89BE2B31C684864ED0E1E5D6F978928182C9E9E8  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	Il2CppChar ___m_value;
};
struct DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E 
{
	int64_t ___Value;
};
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	double ___m_value;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3 
{
	int64_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E 
{
	void* ___begin;
	int32_t ___length;
};
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	float ___m_value;
};
struct UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B 
{
	uint32_t ___m_value;
};
struct UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF 
{
	uint64_t ___m_value;
};
struct UIntPtr_t 
{
	void* ____pointer;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 
{
	uint8_t ___m_Reserved;
};
struct Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8 
{
	void* ___Next;
	int32_t ___Id;
	int32_t ___Version;
	int32_t ___AllocationFlags;
};
struct ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 
{
	intptr_t ____value;
};
struct Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 
{
	bool ___hasValue;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___value;
};
struct AudioDataLoadState_tC19F28D3D0EFE5FD5A6F6B0DBC2253A487A7DC46 
{
	int32_t ___value__;
};
struct AudioSpeakerMode_tD681BDF379A4FB499C5562BF3F97AB93B4E540ED 
{
	int32_t ___value__;
};
struct ControlFunction_tF56532B76DBED412D41D1DC56F7002D533A56F87 
{
	uint32_t ___value__;
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC 
{
	intptr_t ___handle;
};
struct Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 
{
	intptr_t ___m_Node;
	int32_t ___Version;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E 
{
	intptr_t ___m_Handle;
	uint32_t ___m_Version;
};
struct PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 
{
	intptr_t ___m_Handle;
	uint32_t ___m_Version;
};
struct PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 
{
	intptr_t ___m_Handle;
	uint32_t ___m_Version;
};
struct ProcessorFunction_t94187A2444D8312007E01AC295FBB4EFF25E4718 
{
	uint32_t ___value__;
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct Message_tBD40926CA51B245E5907EA3415980470455D8945 
{
	int64_t ___TypeHash;
	void* ___Data;
	intptr_t ___ManagedHandle;
};
struct UpdateSetting_tA679554B453B4685917A94E3D213100F94791EEE 
{
	int32_t ___value__;
};
struct ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 
{
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 ____pointer;
	int32_t ____length;
};
struct AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Handle;
};
struct AudioConfiguration_t4C8585EA98B6559B306B6FAD6A68088555394A5D 
{
	int32_t ___speakerMode;
	int32_t ___dspBufferSize;
	int32_t ___sampleRate;
	int32_t ___numRealVoices;
	int32_t ___numVirtualVoices;
};
struct AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Handle;
};
struct AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 
{
	PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 ___m_Handle;
};
struct AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243 
{
	ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___m_Header;
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___m_Handle;
};
struct ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67 
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___Handle;
	intptr_t ___ManagedTransport;
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Handle;
};
struct PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4 
{
	String_t* ___m_StreamName;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___m_SourceObject;
	Type_t* ___m_SourceBindingType;
	CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509* ___m_CreateOutputMethod;
};
struct PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4_marshaled_pinvoke
{
	char* ___m_StreamName;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke ___m_SourceObject;
	Type_t* ___m_SourceBindingType;
	Il2CppMethodPointer ___m_CreateOutputMethod;
};
struct PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4_marshaled_com
{
	Il2CppChar* ___m_StreamName;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com* ___m_SourceObject;
	Type_t* ___m_SourceBindingType;
	Il2CppMethodPointer ___m_CreateOutputMethod;
};
struct PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 
{
	PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 ___m_Handle;
};
struct ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 
{
	void* ___m_Control;
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___DualThreadHandle;
	void* ___NativeProcessorFunction;
	void* ___NativeControlFunction;
	intptr_t ___ProcessorReflectionData;
	intptr_t ___ControlReflectionData;
};
struct ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___Handle;
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085* ___Header;
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 
{
	int32_t ___speakerMode;
	int32_t ___sampleRate;
};
struct CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 
{
	int32_t ___U3CcontrolUpdateSettingU3Ek__BackingField;
	int32_t ___U3CrealtimeUpdateSettingU3Ek__BackingField;
};
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	String_t* ____paramName;
};
struct AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20  : public AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A
{
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___m_PCMReaderCallback;
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___m_PCMSetPositionCallback;
};
struct AudioFormat_tC9651BFD36B41D1FC782C702B6C980D9768F699D 
{
	AudioConfiguration_t4C8585EA98B6559B306B6FAD6A68088555394A5D ___m_Config;
};
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};
struct GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 
{
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___m_ProcessorInstance;
};
struct InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E  : public MulticastDelegate_t
{
};
struct PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072  : public MulticastDelegate_t
{
};
struct SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30  : public MulticastDelegate_t
{
};
struct AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177  : public MulticastDelegate_t
{
};
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 ___Setup;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 ___Properties;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___ReportedLength;
	bool ___IsFinite;
	bool ___IsRealtime;
	bool ___HasKnownLength;
};
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 ___Setup;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 ___Properties;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___ReportedLength;
	int32_t ___IsFinite;
	int32_t ___IsRealtime;
	int32_t ___HasKnownLength;
};
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 ___Setup;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 ___Properties;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___ReportedLength;
	int32_t ___IsFinite;
	int32_t ___IsRealtime;
	int32_t ___HasKnownLength;
};
struct CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509  : public MulticastDelegate_t
{
};
struct Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5 
{
	bool ___hasValue;
	AudioFormat_tC9651BFD36B41D1FC782C702B6C980D9768F699D ___value;
};
struct AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};
struct GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0 
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 ___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 ___Configuration;
};
struct GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_pinvoke
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 ___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke ___Configuration;
};
struct GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_com
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 ___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com ___Configuration;
};
struct AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35  : public AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941
{
};
struct AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299  : public AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941
{
};
struct AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields
{
	AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* ___OnAudioConfigurationChanged;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___OnAudioSystemShuttingDown;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___OnAudioSystemStartedUp;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1;
};
struct DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E_StaticFields
{
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___Zero;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___MinValue;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___MaxValue;
	int32_t ___TicksPerSecondBits;
	int32_t ___NonPow2TpsBits;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
struct UIntPtr_t_StaticFields
{
	uintptr_t ___Zero;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject;
};
struct PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_StaticFields
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Null;
};
struct PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883_StaticFields
{
	PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 ___m_Null;
};
struct Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_StaticFields
{
	Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F ___m_NullPlayable;
};
struct PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4_StaticFields
{
	PlayableBindingU5BU5D_tC50C3F27A8E4246488F7A5998CAABAC4811A92CD* ___None;
	double ___DefaultDuration;
};
struct PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680_StaticFields
{
	PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 ___m_NullPlayableOutput;
};
struct Type_t_StaticFields
{
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder;
	Il2CppChar ___Delimiter;
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes;
	RuntimeObject* ___Missing;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C  : public RuntimeArray
{
	ALIGN_FIELD (8) float m_Items[1];

	inline float GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline float* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, float value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline float GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline float* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, float value)
	{
		m_Items[index] = value;
	}
};
struct AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B  : public RuntimeArray
{
	ALIGN_FIELD (8) AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* m_Items[1];

	inline AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};

IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_back(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_cleanup(Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_back(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_cleanup(Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled);

IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline (RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Unmarshal_UnmarshalUnityObject_TisRuntimeObject_m2B04FEB8E4BB254DD6F827F3111C91F2AF16D04A_gshared_inline (intptr_t ___0_gcHandlePtr, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t MarshalledUnityObject_Marshal_TisRuntimeObject_m286B34400A212037E8EBD53DBFEAD7D23CDE8051_gshared_inline (RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_gshared (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_gshared_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_gshared_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayableExtensions_SetDuration_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m67FC8BE5E310D54C91DB1D492F7393237401332E_gshared (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_playable, double ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PlayableHandle_IsPlayableOfType_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m95809D16DD3494AEB0E9E652B42E0CE3167BEE0E_gshared (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar* ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_gshared (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_gshared_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PlayableHandle_IsPlayableOfType_TisAudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C_m28F89BE3D7CCE69B28258CC5B1C7D4E814F48CAB_gshared (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PlayableOutputHandle_IsPlayableOutputOfType_TisAudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20_mAC4BC6AF8D183B1A972EA239A6D9BEA34054C7C6_gshared (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_gshared_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, Il2CppChar* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* UnsafeUtility_As_TisIntPtr_t_TisGCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC_mD94F597CB91662C823496DA9FA9A7CF908734A78_gshared_inline (intptr_t* ___0_from, const RuntimeMethod* method) ;

IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_inline (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioResource__ctor_m67FD564B101E07B1EA22EF27F3B5FA290A9D1409 (AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A* __this, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_inline (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*, const RuntimeMethod*))MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF (RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioClip_get_length_Injected_mA4C188DE35DD5721756C2316D48A593C854AFF53 (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_samples_Injected_m18E5C4BD533706C80A8780D7439E151E305479F5 (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_frequency_Injected_m88A5AA18CEDCC1F528AF69A95C23ED5ADD39136B (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_LoadAudioData_Injected_m5157E7E4C3131658BBED2F2A0BB37A33C90F90C5 (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_loadState_Injected_mE83FEBDBF5BE4E2DE89FBF65426FD039F711BDBC (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_inline (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_inline (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Behaviour__ctor_m00422B6EFEA829BCB116D715E74F1EAD2CB6F4F8 (Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01 (RuntimeObject* ___0_obj, String_t* ___1_parameterName, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*, const RuntimeMethod*))MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_GetPitch_Injected_m0D4A9670C9E85FD57C31AD1FF82F91CE27D8B4D6 (intptr_t ___0_source, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_SetPitch_Injected_mDE523D3EC8E91EC01A95CDD8DCB3B3383C990AF2 (intptr_t ___0_source, float ___1_pitch, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3 (intptr_t ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5 (intptr_t ___0_source, intptr_t ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Stop_Injected_mA49628DDBC38E8430F141A40A8E34D5CB18A6CCB (intptr_t ___0__unity_self, bool ___1_stopOneShots, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_get_volume_Injected_mF349411AE1957B030941AC3F318643E63CE9465B (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_volume_Injected_m4A5BD8D8EEEF904031C7B543AE841472A16F43AE (intptr_t ___0__unity_self, float ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_GetPitch_m80F6D2BAF966F669253E9231AFCFFC303779913D (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_SetPitch_mE75DEDF8F37301BDA63E0F545A7A00850C24F53E (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, float ___1_pitch, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* AudioSource_get_generatorObject_mAE73958C08DBF60EF07B70460B51BE36951C31FE (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_generatorObject_m5EABBF00B31FE1A6186D08093A9E5841AB280BA9 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AudioSource_get_generatorObject_Injected_mF5A5F744B96E4C2E96463424A92E5AF2F3680221 (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
inline Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* Unmarshal_UnmarshalUnityObject_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m27ACA708E2D42A879C8C665663669217265002CF_inline (intptr_t ___0_gcHandlePtr, const RuntimeMethod* method)
{
	return ((  Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* (*) (intptr_t, const RuntimeMethod*))Unmarshal_UnmarshalUnityObject_TisRuntimeObject_m2B04FEB8E4BB254DD6F827F3111C91F2AF16D04A_gshared_inline)(___0_gcHandlePtr, method);
}
inline intptr_t MarshalledUnityObject_Marshal_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m965B12ED56AA1618C042D16D10B1BA3BFD98FF4D_inline (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*, const RuntimeMethod*))MarshalledUnityObject_Marshal_TisRuntimeObject_m286B34400A212037E8EBD53DBFEAD7D23CDE8051_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_generatorObject_Injected_m6353E3C9497043EC8D1AAB5CFC1F5A5FF221EF96 (intptr_t ___0__unity_self, intptr_t ___1_value, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_Marshal_TisAudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311_m1580BA03AE5DDE6E684BC285BEC86628223AA9C3_inline (AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311*, const RuntimeMethod*))MarshalledUnityObject_Marshal_TisRuntimeObject_m286B34400A212037E8EBD53DBFEAD7D23CDE8051_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_outputAudioMixerGroup_Injected_mF6ED4EDE32B3A6841560099340AE0340715B71AB (intptr_t ___0__unity_self, intptr_t ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Stop_m8A4872F0A2680798CD28894DD28609445C4783F5 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, bool ___0_stopOneShots, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Pause_Injected_m6A35224283B7AA64A63FA0EB18BC263F26E84810 (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_UnPause_Injected_mC6085B8B35ED9D18A62E54DEFAC86087FCA23A9E (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSource_get_isPlaying_Injected_m9CF82B2AA1F4CDBBE7189E07D1719933AB84065F (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSource_get_loop_Injected_m181F6FEE04B9B1170C6881D0557E1262A6335D78 (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC (intptr_t ___0__unity_self, bool ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9 (intptr_t ___0__unity_self, bool ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_spatialBlend_Injected_mE094C32E8F2B48A9D5572528C149EC784B6472A9 (intptr_t ___0__unity_self, float ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_dopplerLevel_Injected_m171FCC5195E16921ABFEB55719E0740A7C0B7738 (intptr_t ___0__unity_self, float ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioSource_get_priority_Injected_m7247E2704DCCA4499C3180C4605CABB3797E5A61 (intptr_t ___0__unity_self, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_priority_Injected_m1DECF2F19AC3A66A19B1C000E0FE91C7D28172F4 (intptr_t ___0__unity_self, int32_t ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioBehaviour__ctor_m6D88837496C42A746A51383F3D6F29CA72A9D309 (AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6 (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, void* ___0_headerThatShouldBeOfResourceType, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExists_mCA17BD1A2B9E776034C7B312CA810B3B739AA04E (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_handle, ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___1_control, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_processorInstance, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249 (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_generatorInstance, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC GCHandle_FromIntPtr_mA7848A4285D007CADC52B6272DB243C8FDFD5FAC (intptr_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool GCHandle_get_IsAllocated_m241908103D8D867E11CCAB73C918729825E86843_inline (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GCHandle_Free_m1320A260E487EB1EA6D95F9E54BFFCB5A4EF83A3 (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398 (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GeneratorInstance_Equals_m7412F772B53B5052FF5070FB2BB2099A93270206 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0 (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t GeneratorInstance_GetHashCode_mBF9BB29BE3D212AAAEF53A02346FED4B0435C7D2 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) ;
inline void Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604 (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___0_value, const RuntimeMethod* method)
{
	((  void (*) (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363*, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E, const RuntimeMethod*))Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_gshared)(__this, ___0_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ProcessorInstance_Equals_mFE0F0DA5B311AD6D34EABD73F61C4AE3014084ED (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 GeneratorInstance_op_Implicit_mB7A6E46130D3BAB42E00F4DD08155EF286A16167_inline (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* ___0_generatorInstance, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C (String_t* ___0_format, RuntimeObject* ___1_arg0, RuntimeObject* ___2_arg1, RuntimeObject* ___3_arg2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
inline bool Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363*, const RuntimeMethod*))Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_gshared_inline)(__this, method);
}
inline DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method)
{
	return ((  DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E (*) (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363*, const RuntimeMethod*))Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_gshared_inline)(__this, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DiscreteTime_op_Inequality_m4C12F6A54E5C6E8BAC45A3903E6C2A3127C6063C_inline (DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___0_lhs, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___1_rhs, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987 (String_t* ___0_format, RuntimeObject* ___1_arg0, RuntimeObject* ___2_arg1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExistsInternal_mF6C33BA9D06B46EAAA47D05E74BA20CC604141A6 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_handle, void* ___1_control, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* ___0_handle, void* ___1_control, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_CreateHandle_mD7CA217BC6DE899D586272C372456EBB7ED2A825 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E ___0_graph, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, bool ___2_looping, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable__ctor_m4E686B92CFA7C6A36AA794B1E8C36B1E60605751 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) ;
inline void PlayableExtensions_SetDuration_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m67FC8BE5E310D54C91DB1D492F7393237401332E (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_playable, double ___1_value, const RuntimeMethod* method)
{
	((  void (*) (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0, double, const RuntimeMethod*))PlayableExtensions_SetDuration_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m67FC8BE5E310D54C91DB1D492F7393237401332E_gshared)(___0_playable, ___1_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 PlayableHandle_get_Null_mF44FE0A71C67054D272F454C91F7E08CBF14A975 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_InternalCreateAudioClipPlayable_mBDAA54F35207F6C62F87CAE268732072C7287616 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, bool ___2_looping, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___3_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PlayableHandle_IsValid_m07631D12846BAAF2CC302E69A28A44BFE9EB5098 (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, const RuntimeMethod* method) ;
inline bool PlayableHandle_IsPlayableOfType_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m95809D16DD3494AEB0E9E652B42E0CE3167BEE0E (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*, const RuntimeMethod*))PlayableHandle_IsPlayableOfType_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m95809D16DD3494AEB0E9E652B42E0CE3167BEE0E_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidCastException__ctor_mED3F9781E6CA47A92C602C896EDDF2F40ECB8644 (InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Playable__ctor_mD2EB35E024816AEED68795D0124EAB30E05BF6C4 (Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F* __this, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 Playable_GetHandle_m39356D23E849DC5428B262092657662C064E04F8 (Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PlayableHandle_op_Equality_m0E6C48A28F75A870AC22ADE3BD42F7F70A43C99C (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_x, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Single_ToString_mE282EDA9CA4F7DF88432D807732837A629D04972 (float* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___0_str0, String_t* ___1_str1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465 (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetVolumeInternal_m71EFEE49486AACBE4F9C936F4898448AB071B55F (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, float ___1_volume, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetVolume_m3553EC43CBA43CA7802292710A1A284A6DEF8FCC (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, float ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetStereoPanInternal_m206A1B777709E25F42C9EF0BEAF3A84D622D4A90 (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, float ___1_stereoPan, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetStereoPan_mB8403E93B06B2ECCD0D4D6B0D55C151D0CCCC494 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, float ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetSpatialBlendInternal_mBAE4A56ACEE90D4732C5D2C5D2D721C65B3DD55B (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, float ___1_spatialBlend, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetSpatialBlend_m72F1B86E4E5940E8587C1EA32BB48463A00810BB (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, float ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetStartDelayInternal_m1A3816547BF61B7448654300EF078D464A5618CD (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, double ___1_delay, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double PlayableHandle_GetDuration_mE3CF9C12CA5A186288A7F1981A92F043100E02A7 (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayableHandle_SetDone_mE04197A8D1657035DC6C70AF7291715557A9FB49 (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, bool ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayableHandle_SetDuration_m4E07F9F3A90B2F3CE10325D9F405993F03CCA08B (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, double ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetPauseDelayInternal_m803360EBD0BB692C31D31D620AAB9C8D0CD7A9D1 (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, double ___1_delay, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayableHandle_SetTime_m6D255AB6779F3DC278813F1C016FE4EBC9F4B1E4 (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, double ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayableHandle_Play_m1EF8E1105EB6A4A9B0D6638A57CDCBCEEDC6FB2D (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_Seek_m2AB327B535C677793BBADEA634B7AAC9442B1391 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, double ___0_startTime, double ___1_startDelay, double ___2_duration, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_Marshal_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m4CF5833467B7EAC854506E5E808202FABD1F15CC_inline (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*, const RuntimeMethod*))MarshalledUnityObject_Marshal_TisRuntimeObject_m286B34400A212037E8EBD53DBFEAD7D23CDE8051_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_InternalCreateAudioClipPlayable_Injected_mEA590F72729B23ADA8665AB7E4F022D59126E76B (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, intptr_t ___1_clip, bool ___2_looping, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___3_handle, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_inline (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, const RuntimeMethod*))MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline)(___0_obj, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool StringMarshaller_TryMarshalEmptyOrNullString_m615203C511071D59295D889AB136575DFFEA90A6_inline (String_t* ___0_s, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_managedSpanWrapper, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 MemoryExtensions_AsSpan_m0EB07912D71097A8B05F586158966837F5C3DB38_inline (String_t* ___0_text, const RuntimeMethod* method) ;
inline Il2CppChar* ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638 (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method)
{
	return ((  Il2CppChar* (*) (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1*, const RuntimeMethod*))ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_gshared)(__this, method);
}
inline int32_t ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1*, const RuntimeMethod*))ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_gshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ManagedSpanWrapper__ctor_mB29647A21BB87EA4DF859E5C2FA2207F47E525D2 (ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* __this, void* ___0_begin, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* AudioMixer_FindMatchingGroups_Injected_m0D316E90F0D1FC826E857AFE66C69B679DFC9500 (intptr_t ___0__unity_self, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_subPath, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixer_SetFloat_Injected_m07F1E9A4591C32164F5F72AC357D25A023A0634D (intptr_t ___0__unity_self, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_name, float ___2_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixer_GetFloat_Injected_m2794EC0D637390AAA8773C47325038AB4ECA8F72 (intptr_t ___0__unity_self, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_name, float* ___2_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_CreateHandle_mCA2A6EF1CC490A8E59C8EE2020D3304D66B96852 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E ___0_graph, int32_t ___1_inputCount, bool ___2_normalizeInputVolumes, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixerPlayable__ctor_m2CDDE33FCE0B3F7D9EB482D76515FBF771285F47 (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixerPlayable_CreateAudioMixerPlayableInternal_mB91DD585A6A2903F01F49719CA0045C8727B5AA9 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, bool ___1_normalizeInputVolumes, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___2_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayableHandle_SetInputCount_m6067CD3616C428F777903FCBFD789060A2185DEE (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, int32_t ___0_value, const RuntimeMethod* method) ;
inline bool PlayableHandle_IsPlayableOfType_TisAudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C_m28F89BE3D7CCE69B28258CC5B1C7D4E814F48CAB (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*, const RuntimeMethod*))PlayableHandle_IsPlayableOfType_TisAudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C_m28F89BE3D7CCE69B28258CC5B1C7D4E814F48CAB_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57 (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CreateOutputMethod__ctor_m5A339017CD8ECB0140EB936FD2A5B589B20166B4 (CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4 PlayableBinding_CreateInternal_m63C8C98E659B52725E0373019ECFCEC22167A47A (String_t* ___0_name, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_sourceObject, Type_t* ___2_sourceType, CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509* ___3_createFunction, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 AudioPlayableOutput_Create_m90DF38B28813932D4246094FD4DB6105572619D2 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E ___0_graph, String_t* ___1_name, AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___2_target, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 AudioPlayableOutput_op_Implicit_mD2D35763126BDE08E10CA74D8E8C49988477F428 (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 ___0_output, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioPlayableGraphExtensions_InternalCreateAudioOutput_Injected_m4290FFF24DD0F825CC4036371953C9B67865ED3C (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_name, PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___2_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioPlayableGraphExtensions_InternalCreateAudioOutput_m5EF8B18878AFFE0B462E615C8243C2433A3F5E78 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, String_t* ___1_name, PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___2_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 AudioPlayableOutput_get_Null_m8404386CE6506C8C0574B74023EB1BC17A45F205 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput__ctor_mBEA3D7EE652908558720EDC0D40F7BF3EC50D409 (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* __this, PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_SetTarget_m34EE86E5C2833F12993681ABE5AC85961836DE04 (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* __this, AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PlayableOutputHandle_IsValid_mA94C343D72B72C3B1C7636C93A8DB5EA71691312 (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* __this, const RuntimeMethod* method) ;
inline bool PlayableOutputHandle_IsPlayableOutputOfType_TisAudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20_mAC4BC6AF8D183B1A972EA239A6D9BEA34054C7C6 (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883*, const RuntimeMethod*))PlayableOutputHandle_IsPlayableOutputOfType_TisAudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20_mAC4BC6AF8D183B1A972EA239A6D9BEA34054C7C6_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 PlayableOutputHandle_get_Null_m656E8D2549FA031DA8A2EA5B39CE3B33D75B69F8 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 AudioPlayableOutput_GetHandle_m55153D572F8FB9BCFF3843402A20280273B934AE (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayableOutput__ctor_m55FBB20EC479F67641835EA48D84A1AB3DF39747 (PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680* __this, PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 PlayableOutput_GetHandle_m12FF2889D15884CBEB92A6CB376827EBA5A607BF (PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_InternalSetTarget_m3A9912A00BC052FCEDEBB5EB75DCBE5B2B9DA86E (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___0_output, AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___1_target, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_InternalSetEvaluateOnSeek_m639F527A20B97D2617898E223B5CDC1DC8548804 (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___0_output, bool ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_SetEvaluateOnSeek_mB3266A8A68E94933A82A0C431B7A2E7321929D92 (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* __this, bool ___0_value, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_Marshal_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m792F1F32E543E9AE922C6F04B2616F0690E1D1CB_inline (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*, const RuntimeMethod*))MarshalledUnityObject_Marshal_TisRuntimeObject_m286B34400A212037E8EBD53DBFEAD7D23CDE8051_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_InternalSetTarget_Injected_m56A381E3FFAEA1292A2D4C09ECE08D06FEE25ADC (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___0_output, intptr_t ___1_target, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void* IntPtr_op_Explicit_m2728CBA081E79B97DDCF1D4FAD77B309CA1E94BF (intptr_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_Equals_mDE217E745B1016A74BCE82848675ED913B391B8F (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t IntPtr_op_Explicit_mC33775570B5AC86421DABA8F8B9885DBFF49B02F (intptr_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR uintptr_t UIntPtr_op_Explicit_mF1E7911DD5AC13B5E59EE8C7903469D12A3861E8 (uint64_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void* UIntPtr_op_Explicit_m42C3EA82465934F505B4274A7CE320550A48B7B9 (uintptr_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar* String_GetRawStringData_m87BC50B7B314C055E27A28032D1003D42FDE411D (String_t* __this, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, Il2CppChar* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1*, Il2CppChar*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_gshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC Unmarshal_FromIntPtrUnsafe_mB304834C5EC431CC0FBE13C6BB97066F222BC55C_inline (intptr_t ___0_gcHandle, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* GCHandle_get_Target_m481F9508DA5E384D33CD1F4450060DC56BBD4CD5_inline (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_inline (RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (RuntimeObject*, const RuntimeMethod*))MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline)(___0_obj, method);
}
inline GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* UnsafeUtility_As_TisIntPtr_t_TisGCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC_mD94F597CB91662C823496DA9FA9A7CF908734A78_inline (intptr_t* ___0_from, const RuntimeMethod* method)
{
	return ((  GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* (*) (intptr_t*, const RuntimeMethod*))UnsafeUtility_As_TisIntPtr_t_TisGCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC_mD94F597CB91662C823496DA9FA9A7CF908734A78_gshared_inline)(___0_from, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool GCHandle_CanDereferenceHandle_mAAAC42D1268CEF3FDD040A3D1574773D08140579_inline (intptr_t ___0_handle, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* GCHandle_GetRef_mAC7E58E62417209DC41C99F66BA70F0C3AA18DA8_inline (intptr_t ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* GCHandle_GetTarget_mE0AF851834410E2AEA6285B2497751570236C794 (intptr_t ___0_handle, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSettings_InvokeOnAudioConfigurationChanged_m8273D3AEB24F4C3E374238B6F699BE6696808E85 (bool ___0_deviceWasChanged, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* L_0 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioConfigurationChanged;
		V_0 = (bool)((!(((RuntimeObject*)(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0019;
		}
	}
	{
		AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* L_2 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioConfigurationChanged;
		bool L_3 = ___0_deviceWasChanged;
		NullCheck(L_2);
		AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_inline(L_2, L_3, NULL);
	}

IL_0019:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSettings_InvokeOnAudioSystemShuttingDown_m1B9895D60B3267EBDEC69B9169730DBAD8325E90 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B2_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B1_0 = NULL;
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioSystemShuttingDown;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000b;
		}
		G_B1_0 = L_1;
	}
	{
		goto IL_0011;
	}

IL_000b:
	{
		NullCheck(G_B2_0);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(G_B2_0, NULL);
	}

IL_0011:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSettings_InvokeOnAudioSystemStartedUp_m7FE042936237E5BDCB20299D8C4CF583B661468C (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B2_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B1_0 = NULL;
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioSystemStartedUp;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000b;
		}
		G_B1_0 = L_1;
	}
	{
		goto IL_0011;
	}

IL_000b:
	{
		NullCheck(G_B2_0);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(G_B2_0, NULL);
	}

IL_0011:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_Multicast(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* currentDelegate = reinterpret_cast<AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_deviceWasChanged, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_OpenInst(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_deviceWasChanged, method);
}
void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_OpenStatic(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_deviceWasChanged, method);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177 (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(int32_t);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	il2cppPInvokeFunc(static_cast<int32_t>(___0_deviceWasChanged));

}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler__ctor_mA9827AB9472EC8EE0A0F0FC24EBC06B4740DD944 (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		if (___0_object == NULL)
			il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
		__this->___invoke_impl = __this->___method_ptr;
		__this->___method_code = (intptr_t)__this->___m_target;
	}
	__this->___extra_arg = (intptr_t)&AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_Multicast;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2 (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_deviceWasChanged, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip__ctor_m038DA97CB07076D1D9391E1E103F0F41D3622F89 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	{
		__this->___m_PCMReaderCallback = (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_PCMReaderCallback), (void*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)NULL);
		__this->___m_PCMSetPositionCallback = (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_PCMSetPositionCallback), (void*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)NULL);
		AudioResource__ctor_m67FD564B101E07B1EA22EF27F3B5FA290A9D1409(__this, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		float L_2;
		L_2 = AudioClip_get_length_Injected_mA4C188DE35DD5721756C2316D48A593C854AFF53(G_B2_0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_samples_mDEA01CA75E7DEA0F8D480E4AF97FB96085BCF38E (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		int32_t L_2;
		L_2 = AudioClip_get_samples_Injected_m18E5C4BD533706C80A8780D7439E151E305479F5(G_B2_0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_frequency_m6647E10F4B2B1335187B0066E82468CCCF19647B (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		int32_t L_2;
		L_2 = AudioClip_get_frequency_Injected_m88A5AA18CEDCC1F528AF69A95C23ED5ADD39136B(G_B2_0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_LoadAudioData_mF43E6195AA70C39045DCF08D01C61C9DAA6876DC (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		bool L_2;
		L_2 = AudioClip_LoadAudioData_Injected_m5157E7E4C3131658BBED2F2A0BB37A33C90F90C5(G_B2_0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_loadState_mD5E89ED3E6C1C706C021598FDF86FEB7BF5DE669 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		int32_t L_2;
		L_2 = AudioClip_get_loadState_Injected_mE83FEBDBF5BE4E2DE89FBF65426FD039F711BDBC(G_B2_0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_InvokePCMReaderCallback_Internal_m766E5705AB5AE16F5F142867CC3758ABE4BF462C (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_0 = __this->___m_PCMReaderCallback;
		V_0 = (bool)((!(((RuntimeObject*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001b;
		}
	}
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_2 = __this->___m_PCMReaderCallback;
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_3 = ___0_data;
		NullCheck(L_2);
		PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_inline(L_2, L_3, NULL);
	}

IL_001b:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_InvokePCMSetPositionCallback_Internal_m986EF703B7DDE42343730DE93A095D05B9F4DBB8 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, int32_t ___0_position, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_0 = __this->___m_PCMSetPositionCallback;
		V_0 = (bool)((!(((RuntimeObject*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001b;
		}
	}
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_2 = __this->___m_PCMSetPositionCallback;
		int32_t L_3 = ___0_position;
		NullCheck(L_2);
		PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_inline(L_2, L_3, NULL);
	}

IL_001b:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isRealtime_mAC78FD95FB9998B38D0E50C2462DC9D9147FEAA4 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	{
		NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* L_0 = (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var)));
		NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isRealtime_mAC78FD95FB9998B38D0E50C2462DC9D9147FEAA4_RuntimeMethod_var)));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isFinite_m20E61329E86505BA44C480115CD9A403FC63308C (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	{
		NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* L_0 = (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var)));
		NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isFinite_m20E61329E86505BA44C480115CD9A403FC63308C_RuntimeMethod_var)));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_length_mCA231C69B56E93689C2B45DCE2C3F0AD45CF541B (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* L_0 = (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var)));
		NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_length_mCA231C69B56E93689C2B45DCE2C3F0AD45CF541B_RuntimeMethod_var)));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 AudioClip_UnityEngine_Audio_IAudioGenerator_CreateInstance_m78256DB80A6F6DB7AEF47DB8A3E827723D228FD5 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243 ___0_context, Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5 ___1_nestedFormat, CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 ___2_parameters, const RuntimeMethod* method) 
{
	{
		NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* L_0 = (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var)));
		NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_UnityEngine_Audio_IAudioGenerator_CreateInstance_m78256DB80A6F6DB7AEF47DB8A3E827723D228FD5_RuntimeMethod_var)));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioClip_get_length_Injected_mA4C188DE35DD5721756C2316D48A593C854AFF53 (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef float (*AudioClip_get_length_Injected_mA4C188DE35DD5721756C2316D48A593C854AFF53_ftn) (intptr_t);
	static AudioClip_get_length_Injected_mA4C188DE35DD5721756C2316D48A593C854AFF53_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_length_Injected_mA4C188DE35DD5721756C2316D48A593C854AFF53_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_length_Injected(System.IntPtr)");
	float icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_samples_Injected_m18E5C4BD533706C80A8780D7439E151E305479F5 (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioClip_get_samples_Injected_m18E5C4BD533706C80A8780D7439E151E305479F5_ftn) (intptr_t);
	static AudioClip_get_samples_Injected_m18E5C4BD533706C80A8780D7439E151E305479F5_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_samples_Injected_m18E5C4BD533706C80A8780D7439E151E305479F5_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_samples_Injected(System.IntPtr)");
	int32_t icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_frequency_Injected_m88A5AA18CEDCC1F528AF69A95C23ED5ADD39136B (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioClip_get_frequency_Injected_m88A5AA18CEDCC1F528AF69A95C23ED5ADD39136B_ftn) (intptr_t);
	static AudioClip_get_frequency_Injected_m88A5AA18CEDCC1F528AF69A95C23ED5ADD39136B_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_frequency_Injected_m88A5AA18CEDCC1F528AF69A95C23ED5ADD39136B_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_frequency_Injected(System.IntPtr)");
	int32_t icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_LoadAudioData_Injected_m5157E7E4C3131658BBED2F2A0BB37A33C90F90C5 (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef bool (*AudioClip_LoadAudioData_Injected_m5157E7E4C3131658BBED2F2A0BB37A33C90F90C5_ftn) (intptr_t);
	static AudioClip_LoadAudioData_Injected_m5157E7E4C3131658BBED2F2A0BB37A33C90F90C5_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_LoadAudioData_Injected_m5157E7E4C3131658BBED2F2A0BB37A33C90F90C5_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::LoadAudioData_Injected(System.IntPtr)");
	bool icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_loadState_Injected_mE83FEBDBF5BE4E2DE89FBF65426FD039F711BDBC (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioClip_get_loadState_Injected_mE83FEBDBF5BE4E2DE89FBF65426FD039F711BDBC_ftn) (intptr_t);
	static AudioClip_get_loadState_Injected_mE83FEBDBF5BE4E2DE89FBF65426FD039F711BDBC_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_loadState_Injected_mE83FEBDBF5BE4E2DE89FBF65426FD039F711BDBC_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_loadState_Injected(System.IntPtr)");
	int32_t icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_Multicast(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* currentDelegate = reinterpret_cast<PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_data, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenInst(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	NullCheck(___0_data);
	typedef void (*FunctionPointerType) (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_data, method);
}
void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenStatic(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_data, method);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(float*);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	float* ____0_data_marshaled = NULL;
	if (___0_data != NULL)
	{
		____0_data_marshaled = reinterpret_cast<float*>((___0_data)->GetAddressAtUnchecked(0));
	}

	il2cppPInvokeFunc(____0_data_marshaled);

}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMReaderCallback__ctor_mF621B6CC1A4BA6525190C5037401CF2FD5C0CF28 (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		bool isOpen = parameterCount == 0;
		if (isOpen)
		{
			__this->___invoke_impl = (intptr_t)&PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenInst;
		}
		else
		{
			if (___0_object == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			__this->___invoke_impl = __this->___method_ptr;
			__this->___method_code = (intptr_t)__this->___m_target;
		}
	}
	__this->___extra_arg = (intptr_t)&PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_Multicast;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152 (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_data, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_Multicast(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* currentDelegate = reinterpret_cast<PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_position, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_OpenInst(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_position, method);
}
void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_OpenStatic(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_position, method);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072 (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(int32_t);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	il2cppPInvokeFunc(___0_position);

}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMSetPositionCallback__ctor_mD16F77DDB552EB69BB3F5EF39420B2F09F95455B (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		if (___0_object == NULL)
			il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
		__this->___invoke_impl = __this->___method_ptr;
		__this->___method_code = (intptr_t)__this->___m_target;
	}
	__this->___extra_arg = (intptr_t)&PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_Multicast;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702 (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_position, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioBehaviour__ctor_m6D88837496C42A746A51383F3D6F29CA72A9D309 (AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941* __this, const RuntimeMethod* method) 
{
	{
		Behaviour__ctor_m00422B6EFEA829BCB116D715E74F1EAD2CB6F4F8(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_GetPitch_m80F6D2BAF966F669253E9231AFCFFC303779913D (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B4_0;
	memset((&G_B4_0), 0, sizeof(G_B4_0));
	intptr_t G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_0 = ___0_source;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_1 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_1, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
	}

IL_000e:
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_2 = ___0_source;
		intptr_t L_3;
		L_3 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(L_2, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_4 = L_3;
		if (L_4)
		{
			G_B4_0 = L_4;
			goto IL_0023;
		}
		G_B3_0 = L_4;
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_5 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_5, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
		G_B4_0 = G_B3_0;
	}

IL_0023:
	{
		float L_6;
		L_6 = AudioSource_GetPitch_Injected_m0D4A9670C9E85FD57C31AD1FF82F91CE27D8B4D6(G_B4_0, NULL);
		return L_6;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_SetPitch_mE75DEDF8F37301BDA63E0F545A7A00850C24F53E (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, float ___1_pitch, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B4_0;
	memset((&G_B4_0), 0, sizeof(G_B4_0));
	intptr_t G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_0 = ___0_source;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_1 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_1, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
	}

IL_000e:
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_2 = ___0_source;
		intptr_t L_3;
		L_3 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(L_2, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_4 = L_3;
		if (L_4)
		{
			G_B4_0 = L_4;
			goto IL_0023;
		}
		G_B3_0 = L_4;
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_5 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_5, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
		G_B4_0 = G_B3_0;
	}

IL_0023:
	{
		float L_6 = ___1_pitch;
		AudioSource_SetPitch_Injected_mDE523D3EC8E91EC01A95CDD8DCB3B3383C990AF2(G_B4_0, L_6, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B4_0;
	memset((&G_B4_0), 0, sizeof(G_B4_0));
	intptr_t G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_0 = ___0_source;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_1 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_1, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
	}

IL_000e:
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_2 = ___0_source;
		intptr_t L_3;
		L_3 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(L_2, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_4 = L_3;
		if (L_4)
		{
			G_B4_0 = L_4;
			goto IL_0023;
		}
		G_B3_0 = L_4;
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_5 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_5, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
		G_B4_0 = G_B3_0;
	}

IL_0023:
	{
		uint64_t L_6 = ___1_delay;
		AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3(G_B4_0, L_6, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD5F2B3561CFA951FF37495C4B6AE6DB436EC2259);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B6_0;
	memset((&G_B6_0), 0, sizeof(G_B6_0));
	intptr_t G_B5_0;
	memset((&G_B5_0), 0, sizeof(G_B5_0));
	intptr_t G_B8_0;
	memset((&G_B8_0), 0, sizeof(G_B8_0));
	intptr_t G_B8_1;
	memset((&G_B8_1), 0, sizeof(G_B8_1));
	intptr_t G_B7_0;
	memset((&G_B7_0), 0, sizeof(G_B7_0));
	intptr_t G_B7_1;
	memset((&G_B7_1), 0, sizeof(G_B7_1));
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_0 = ___0_source;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_1 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_1, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
	}

IL_000e:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_2 = ___1_clip;
		if (L_2)
		{
			goto IL_001d;
		}
	}
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = ___1_clip;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_3, _stringLiteralD5F2B3561CFA951FF37495C4B6AE6DB436EC2259, NULL);
	}

IL_001d:
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_4 = ___0_source;
		intptr_t L_5;
		L_5 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(L_4, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_6 = L_5;
		if (L_6)
		{
			G_B6_0 = L_6;
			goto IL_0032;
		}
		G_B5_0 = L_6;
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_7 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_7, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
		G_B6_0 = G_B5_0;
	}

IL_0032:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_8 = ___1_clip;
		intptr_t L_9;
		L_9 = MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_inline(L_8, MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		intptr_t L_10 = L_9;
		if (L_10)
		{
			G_B8_0 = L_10;
			G_B8_1 = G_B6_0;
			goto IL_0047;
		}
		G_B7_0 = L_10;
		G_B7_1 = G_B6_0;
	}
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_11 = ___1_clip;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_11, _stringLiteralD5F2B3561CFA951FF37495C4B6AE6DB436EC2259, NULL);
		G_B8_0 = G_B7_0;
		G_B8_1 = G_B7_1;
	}

IL_0047:
	{
		float L_12 = ___2_volumeScale;
		AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5(G_B8_1, G_B8_0, L_12, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Stop_m8A4872F0A2680798CD28894DD28609445C4783F5 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, bool ___0_stopOneShots, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		bool L_2 = ___0_stopOneShots;
		AudioSource_Stop_Injected_mA49628DDBC38E8430F141A40A8E34D5CB18A6CCB(G_B2_0, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_get_volume_m9CCF33BC636562EA282FDE07463B547D70134EE3 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		float L_2;
		L_2 = AudioSource_get_volume_Injected_mF349411AE1957B030941AC3F318643E63CE9465B(G_B2_0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_volume_mD902BBDBBDE0E3C148609BF3C05096148E90F2C0 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, float ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		float L_2 = ___0_value;
		AudioSource_set_volume_Injected_m4A5BD8D8EEEF904031C7B543AE841472A16F43AE(G_B2_0, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_get_pitch_mB1B0B8A52400B5C798BF1E644FE1C2FFA20A9863 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		float L_0;
		L_0 = AudioSource_GetPitch_m80F6D2BAF966F669253E9231AFCFFC303779913D(__this, NULL);
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		float L_1 = V_0;
		return L_1;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_pitch_mD14631FC99BF38AAFB356D9C45546BC16CF9E811 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, float ___0_value, const RuntimeMethod* method) 
{
	{
		float L_0 = ___0_value;
		AudioSource_SetPitch_mE75DEDF8F37301BDA63E0F545A7A00850C24F53E(__this, L_0, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioSource_get_clip_m4F5027066F9FC44B44192713142B0C277BB418FE (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0;
		L_0 = AudioSource_get_generatorObject_mAE73958C08DBF60EF07B70460B51BE36951C31FE(__this, NULL);
		return ((AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*)IsInstSealed((RuntimeObject*)L_0, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_il2cpp_TypeInfo_var));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_clip_mFF441895E274286C88D9C75ED5CA1B1B39528D70 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_value, const RuntimeMethod* method) 
{
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = ___0_value;
		AudioSource_set_generatorObject_m5EABBF00B31FE1A6186D08093A9E5841AB280BA9(__this, L_0, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* AudioSource_get_generatorObject_mAE73958C08DBF60EF07B70460B51BE36951C31FE (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Unmarshal_UnmarshalUnityObject_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m27ACA708E2D42A879C8C665663669217265002CF_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		intptr_t L_2;
		L_2 = AudioSource_get_generatorObject_Injected_mF5A5F744B96E4C2E96463424A92E5AF2F3680221(G_B2_0, NULL);
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_3;
		L_3 = Unmarshal_UnmarshalUnityObject_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m27ACA708E2D42A879C8C665663669217265002CF_inline(L_2, Unmarshal_UnmarshalUnityObject_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m27ACA708E2D42A879C8C665663669217265002CF_RuntimeMethod_var);
		return L_3;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_generatorObject_m5EABBF00B31FE1A6186D08093A9E5841AB280BA9 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_Marshal_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m965B12ED56AA1618C042D16D10B1BA3BFD98FF4D_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_2 = ___0_value;
		intptr_t L_3;
		L_3 = MarshalledUnityObject_Marshal_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m965B12ED56AA1618C042D16D10B1BA3BFD98FF4D_inline(L_2, MarshalledUnityObject_Marshal_TisObject_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_m965B12ED56AA1618C042D16D10B1BA3BFD98FF4D_RuntimeMethod_var);
		AudioSource_set_generatorObject_Injected_m6353E3C9497043EC8D1AAB5CFC1F5A5FF221EF96(G_B2_0, L_3, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_outputAudioMixerGroup_m10D0A0EAE270424CD2F3BB960CFAA158D9FC24CF (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_Marshal_TisAudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311_m1580BA03AE5DDE6E684BC285BEC86628223AA9C3_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* L_2 = ___0_value;
		intptr_t L_3;
		L_3 = MarshalledUnityObject_Marshal_TisAudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311_m1580BA03AE5DDE6E684BC285BEC86628223AA9C3_inline(L_2, MarshalledUnityObject_Marshal_TisAudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311_m1580BA03AE5DDE6E684BC285BEC86628223AA9C3_RuntimeMethod_var);
		AudioSource_set_outputAudioMixerGroup_Injected_mF6ED4EDE32B3A6841560099340AE0340715B71AB(G_B2_0, L_3, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_m95DF07111C61D0E0F00257A00384D31531D590C3 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	{
		AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960(__this, ((int64_t)0), NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShot_mF6FE95C58996B38EF6E7F7482F95F5E15E0AB30B (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, float ___1_volumeScale, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral49403A17E8D32B35CB5B66AB1A2651A7EEAD1B00);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = ___0_clip;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		V_0 = L_1;
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_001a;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral49403A17E8D32B35CB5B66AB1A2651A7EEAD1B00, NULL);
		goto IL_0023;
	}

IL_001a:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = ___0_clip;
		float L_4 = ___1_volumeScale;
		AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265(__this, L_3, L_4, NULL);
	}

IL_0023:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Stop_m318F17F17A147C77FF6E0A5A7A6BE057DB90F537 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	{
		AudioSource_Stop_m8A4872F0A2680798CD28894DD28609445C4783F5(__this, (bool)1, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Pause_m2C2A09359E8AA924FEADECC1AFEA519B3C915B26 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		AudioSource_Pause_Injected_m6A35224283B7AA64A63FA0EB18BC263F26E84810(G_B2_0, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_UnPause_mC4A6A1E71439A3ADB4664B62DABDF4D79D3B21B9 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		AudioSource_UnPause_Injected_mC6085B8B35ED9D18A62E54DEFAC86087FCA23A9E(G_B2_0, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSource_get_isPlaying_mC203303F2F7146B2C056CB47B9391463FDF408FC (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		bool L_2;
		L_2 = AudioSource_get_isPlaying_Injected_m9CF82B2AA1F4CDBBE7189E07D1719933AB84065F(G_B2_0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSource_get_loop_m2D83BF58E1BD1BEE4CC80413C12E761D3310FC2C (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		bool L_2;
		L_2 = AudioSource_get_loop_Injected_m181F6FEE04B9B1170C6881D0557E1262A6335D78(G_B2_0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_loop_m834A590939D8456008C0F897FD80B0ECFFB7FE56 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, bool ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		bool L_2 = ___0_value;
		AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC(G_B2_0, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_playOnAwake_m7EACC6ECEF12D7BA86A4E5A53603F1C8F9E11949 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, bool ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		bool L_2 = ___0_value;
		AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9(G_B2_0, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_spatialBlend_mCEE7A3E87A8C146E048B2CA3413FDC7BDB7BE001 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, float ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		float L_2 = ___0_value;
		AudioSource_set_spatialBlend_Injected_mE094C32E8F2B48A9D5572528C149EC784B6472A9(G_B2_0, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_dopplerLevel_mB9AC5164E5AF16ACECA3B8E29F5C8573C37E40D6 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, float ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		float L_2 = ___0_value;
		AudioSource_set_dopplerLevel_Injected_m171FCC5195E16921ABFEB55719E0740A7C0B7738(G_B2_0, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioSource_get_priority_mD4B6D16F6BCB1D5ACA3F2CC096EDA8861DA66881 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		int32_t L_2;
		L_2 = AudioSource_get_priority_Injected_m7247E2704DCCA4499C3180C4605CABB3797E5A61(G_B2_0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_priority_mD1AB7ED858D8A1233642F5DBA81AEFBE35DD4B40 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		int32_t L_2 = ___0_value;
		AudioSource_set_priority_Injected_m1DECF2F19AC3A66A19B1C000E0FE91C7D28172F4(G_B2_0, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource__ctor_mC67BD65374AC3CDFB702307F4A89932D803191C1 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	{
		AudioBehaviour__ctor_m6D88837496C42A746A51383F3D6F29CA72A9D309(__this, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_GetPitch_Injected_m0D4A9670C9E85FD57C31AD1FF82F91CE27D8B4D6 (intptr_t ___0_source, const RuntimeMethod* method) 
{
	typedef float (*AudioSource_GetPitch_Injected_m0D4A9670C9E85FD57C31AD1FF82F91CE27D8B4D6_ftn) (intptr_t);
	static AudioSource_GetPitch_Injected_m0D4A9670C9E85FD57C31AD1FF82F91CE27D8B4D6_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_GetPitch_Injected_m0D4A9670C9E85FD57C31AD1FF82F91CE27D8B4D6_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::GetPitch_Injected(System.IntPtr)");
	float icallRetVal = _il2cpp_icall_func(___0_source);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_SetPitch_Injected_mDE523D3EC8E91EC01A95CDD8DCB3B3383C990AF2 (intptr_t ___0_source, float ___1_pitch, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_SetPitch_Injected_mDE523D3EC8E91EC01A95CDD8DCB3B3383C990AF2_ftn) (intptr_t, float);
	static AudioSource_SetPitch_Injected_mDE523D3EC8E91EC01A95CDD8DCB3B3383C990AF2_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_SetPitch_Injected_mDE523D3EC8E91EC01A95CDD8DCB3B3383C990AF2_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::SetPitch_Injected(System.IntPtr,System.Single)");
	_il2cpp_icall_func(___0_source, ___1_pitch);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3 (intptr_t ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3_ftn) (intptr_t, uint64_t);
	static AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::PlayHelper_Injected(System.IntPtr,System.UInt64)");
	_il2cpp_icall_func(___0_source, ___1_delay);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5 (intptr_t ___0_source, intptr_t ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5_ftn) (intptr_t, intptr_t, float);
	static AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::PlayOneShotHelper_Injected(System.IntPtr,System.IntPtr,System.Single)");
	_il2cpp_icall_func(___0_source, ___1_clip, ___2_volumeScale);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Stop_Injected_mA49628DDBC38E8430F141A40A8E34D5CB18A6CCB (intptr_t ___0__unity_self, bool ___1_stopOneShots, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_Stop_Injected_mA49628DDBC38E8430F141A40A8E34D5CB18A6CCB_ftn) (intptr_t, bool);
	static AudioSource_Stop_Injected_mA49628DDBC38E8430F141A40A8E34D5CB18A6CCB_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_Stop_Injected_mA49628DDBC38E8430F141A40A8E34D5CB18A6CCB_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::Stop_Injected(System.IntPtr,System.Boolean)");
	_il2cpp_icall_func(___0__unity_self, ___1_stopOneShots);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_get_volume_Injected_mF349411AE1957B030941AC3F318643E63CE9465B (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef float (*AudioSource_get_volume_Injected_mF349411AE1957B030941AC3F318643E63CE9465B_ftn) (intptr_t);
	static AudioSource_get_volume_Injected_mF349411AE1957B030941AC3F318643E63CE9465B_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_get_volume_Injected_mF349411AE1957B030941AC3F318643E63CE9465B_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::get_volume_Injected(System.IntPtr)");
	float icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_volume_Injected_m4A5BD8D8EEEF904031C7B543AE841472A16F43AE (intptr_t ___0__unity_self, float ___1_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_volume_Injected_m4A5BD8D8EEEF904031C7B543AE841472A16F43AE_ftn) (intptr_t, float);
	static AudioSource_set_volume_Injected_m4A5BD8D8EEEF904031C7B543AE841472A16F43AE_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_volume_Injected_m4A5BD8D8EEEF904031C7B543AE841472A16F43AE_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_volume_Injected(System.IntPtr,System.Single)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AudioSource_get_generatorObject_Injected_mF5A5F744B96E4C2E96463424A92E5AF2F3680221 (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef intptr_t (*AudioSource_get_generatorObject_Injected_mF5A5F744B96E4C2E96463424A92E5AF2F3680221_ftn) (intptr_t);
	static AudioSource_get_generatorObject_Injected_mF5A5F744B96E4C2E96463424A92E5AF2F3680221_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_get_generatorObject_Injected_mF5A5F744B96E4C2E96463424A92E5AF2F3680221_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::get_generatorObject_Injected(System.IntPtr)");
	intptr_t icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_generatorObject_Injected_m6353E3C9497043EC8D1AAB5CFC1F5A5FF221EF96 (intptr_t ___0__unity_self, intptr_t ___1_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_generatorObject_Injected_m6353E3C9497043EC8D1AAB5CFC1F5A5FF221EF96_ftn) (intptr_t, intptr_t);
	static AudioSource_set_generatorObject_Injected_m6353E3C9497043EC8D1AAB5CFC1F5A5FF221EF96_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_generatorObject_Injected_m6353E3C9497043EC8D1AAB5CFC1F5A5FF221EF96_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_generatorObject_Injected(System.IntPtr,System.IntPtr)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_outputAudioMixerGroup_Injected_mF6ED4EDE32B3A6841560099340AE0340715B71AB (intptr_t ___0__unity_self, intptr_t ___1_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_outputAudioMixerGroup_Injected_mF6ED4EDE32B3A6841560099340AE0340715B71AB_ftn) (intptr_t, intptr_t);
	static AudioSource_set_outputAudioMixerGroup_Injected_mF6ED4EDE32B3A6841560099340AE0340715B71AB_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_outputAudioMixerGroup_Injected_mF6ED4EDE32B3A6841560099340AE0340715B71AB_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_outputAudioMixerGroup_Injected(System.IntPtr,System.IntPtr)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Pause_Injected_m6A35224283B7AA64A63FA0EB18BC263F26E84810 (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_Pause_Injected_m6A35224283B7AA64A63FA0EB18BC263F26E84810_ftn) (intptr_t);
	static AudioSource_Pause_Injected_m6A35224283B7AA64A63FA0EB18BC263F26E84810_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_Pause_Injected_m6A35224283B7AA64A63FA0EB18BC263F26E84810_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::Pause_Injected(System.IntPtr)");
	_il2cpp_icall_func(___0__unity_self);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_UnPause_Injected_mC6085B8B35ED9D18A62E54DEFAC86087FCA23A9E (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_UnPause_Injected_mC6085B8B35ED9D18A62E54DEFAC86087FCA23A9E_ftn) (intptr_t);
	static AudioSource_UnPause_Injected_mC6085B8B35ED9D18A62E54DEFAC86087FCA23A9E_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_UnPause_Injected_mC6085B8B35ED9D18A62E54DEFAC86087FCA23A9E_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::UnPause_Injected(System.IntPtr)");
	_il2cpp_icall_func(___0__unity_self);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSource_get_isPlaying_Injected_m9CF82B2AA1F4CDBBE7189E07D1719933AB84065F (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef bool (*AudioSource_get_isPlaying_Injected_m9CF82B2AA1F4CDBBE7189E07D1719933AB84065F_ftn) (intptr_t);
	static AudioSource_get_isPlaying_Injected_m9CF82B2AA1F4CDBBE7189E07D1719933AB84065F_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_get_isPlaying_Injected_m9CF82B2AA1F4CDBBE7189E07D1719933AB84065F_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::get_isPlaying_Injected(System.IntPtr)");
	bool icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSource_get_loop_Injected_m181F6FEE04B9B1170C6881D0557E1262A6335D78 (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef bool (*AudioSource_get_loop_Injected_m181F6FEE04B9B1170C6881D0557E1262A6335D78_ftn) (intptr_t);
	static AudioSource_get_loop_Injected_m181F6FEE04B9B1170C6881D0557E1262A6335D78_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_get_loop_Injected_m181F6FEE04B9B1170C6881D0557E1262A6335D78_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::get_loop_Injected(System.IntPtr)");
	bool icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC (intptr_t ___0__unity_self, bool ___1_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC_ftn) (intptr_t, bool);
	static AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_loop_Injected(System.IntPtr,System.Boolean)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9 (intptr_t ___0__unity_self, bool ___1_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9_ftn) (intptr_t, bool);
	static AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_playOnAwake_Injected(System.IntPtr,System.Boolean)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_spatialBlend_Injected_mE094C32E8F2B48A9D5572528C149EC784B6472A9 (intptr_t ___0__unity_self, float ___1_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_spatialBlend_Injected_mE094C32E8F2B48A9D5572528C149EC784B6472A9_ftn) (intptr_t, float);
	static AudioSource_set_spatialBlend_Injected_mE094C32E8F2B48A9D5572528C149EC784B6472A9_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_spatialBlend_Injected_mE094C32E8F2B48A9D5572528C149EC784B6472A9_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_spatialBlend_Injected(System.IntPtr,System.Single)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_dopplerLevel_Injected_m171FCC5195E16921ABFEB55719E0740A7C0B7738 (intptr_t ___0__unity_self, float ___1_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_dopplerLevel_Injected_m171FCC5195E16921ABFEB55719E0740A7C0B7738_ftn) (intptr_t, float);
	static AudioSource_set_dopplerLevel_Injected_m171FCC5195E16921ABFEB55719E0740A7C0B7738_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_dopplerLevel_Injected_m171FCC5195E16921ABFEB55719E0740A7C0B7738_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_dopplerLevel_Injected(System.IntPtr,System.Single)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioSource_get_priority_Injected_m7247E2704DCCA4499C3180C4605CABB3797E5A61 (intptr_t ___0__unity_self, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioSource_get_priority_Injected_m7247E2704DCCA4499C3180C4605CABB3797E5A61_ftn) (intptr_t);
	static AudioSource_get_priority_Injected_m7247E2704DCCA4499C3180C4605CABB3797E5A61_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_get_priority_Injected_m7247E2704DCCA4499C3180C4605CABB3797E5A61_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::get_priority_Injected(System.IntPtr)");
	int32_t icallRetVal = _il2cpp_icall_func(___0__unity_self);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_priority_Injected_m1DECF2F19AC3A66A19B1C000E0FE91C7D28172F4 (intptr_t ___0__unity_self, int32_t ___1_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_priority_Injected_m1DECF2F19AC3A66A19B1C000E0FE91C7D28172F4_ftn) (intptr_t, int32_t);
	static AudioSource_set_priority_Injected_m1DECF2F19AC3A66A19B1C000E0FE91C7D28172F4_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_priority_Injected_m1DECF2F19AC3A66A19B1C000E0FE91C7D28172F4_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_priority_Injected(System.IntPtr,System.Int32)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSampleProvider_InvokeSampleFramesAvailable_mEB16F7230AB65A3576BF053AC5719F8E134FBCD4 (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* __this, int32_t ___0_sampleFrameCount, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_0 = __this->___sampleFramesAvailable;
		V_0 = (bool)((!(((RuntimeObject*)(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001c;
		}
	}
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_2 = __this->___sampleFramesAvailable;
		int32_t L_3 = ___0_sampleFrameCount;
		NullCheck(L_2);
		SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline(L_2, __this, L_3, NULL);
	}

IL_001c:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSampleProvider_InvokeSampleFramesOverflow_m66593173A527981F5EB2A5EF77B0C9119DAB5E15 (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* __this, int32_t ___0_droppedSampleFrameCount, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_0 = __this->___sampleFramesOverflow;
		V_0 = (bool)((!(((RuntimeObject*)(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001c;
		}
	}
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_2 = __this->___sampleFramesOverflow;
		int32_t L_3 = ___0_droppedSampleFrameCount;
		NullCheck(L_2);
		SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline(L_2, __this, L_3, NULL);
	}

IL_001c:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_Multicast(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* currentDelegate = reinterpret_cast<SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_provider, ___1_sampleFrameCount, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInst(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	typedef void (*FunctionPointerType) (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_provider, ___1_sampleFrameCount, method);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenStatic(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_provider, ___1_sampleFrameCount, method);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenVirtual(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	VirtualActionInvoker1< uint32_t >::Invoke(il2cpp_codegen_method_get_slot(method), ___0_provider, ___1_sampleFrameCount);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInterface(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	InterfaceActionInvoker1< uint32_t >::Invoke(il2cpp_codegen_method_get_slot(method), il2cpp_codegen_method_get_declaring_type(method), ___0_provider, ___1_sampleFrameCount);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericVirtual(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	GenericVirtualActionInvoker1< uint32_t >::Invoke(method, ___0_provider, ___1_sampleFrameCount);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericInterface(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	GenericInterfaceActionInvoker1< uint32_t >::Invoke(method, ___0_provider, ___1_sampleFrameCount);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SampleFramesHandler__ctor_m7DDE0BAD439CD80791140C7D42D661B598A7663A (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 2;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
		{
			if (__this->___method_is_virtual)
			{
				if (il2cpp_codegen_method_is_generic_instance_method((RuntimeMethod*)___1_method))
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericInterface;
					else
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericVirtual;
				else
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInterface;
					else
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenVirtual;
			}
			else
			{
				__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInst;
			}
		}
		else
		{
			if (___0_object == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			__this->___invoke_impl = __this->___method_ptr;
			__this->___method_code = (intptr_t)__this->___m_target;
		}
	}
	__this->___extra_arg = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_Multicast;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_provider, ___1_sampleFrameCount, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6 (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, void* ___0_headerThatShouldBeOfResourceType, const RuntimeMethod* method) 
{
	ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* V_0 = NULL;
	{
		void* L_0 = ___0_headerThatShouldBeOfResourceType;
		V_0 = (ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67*)L_0;
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_1 = V_0;
		NullCheck(L_1);
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_2 = L_1->___Handle;
		__this->___m_Handle = L_2;
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_3 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___m_Handle);
		Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(L_3, NULL);
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_4 = V_0;
		__this->___m_Header = L_4;
		return;
	}
}
IL2CPP_EXTERN_C  void ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6_AdjustorThunk (RuntimeObject* __this, void* ___0_headerThatShouldBeOfResourceType, const RuntimeMethod* method)
{
	ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243*>(__this + _offset);
	ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6(_thisAdjusted, ___0_headerThatShouldBeOfResourceType, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_processorInstance, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_0 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___m_Handle);
		Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(L_0, NULL);
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_1 = ___0_processorInstance;
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_2 = L_1.___Handle;
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_3 = __this->___m_Header;
		bool L_4;
		L_4 = ScriptableProcessorBindings_CheckProcessorExists_mCA17BD1A2B9E776034C7B312CA810B3B739AA04E(L_2, L_3, NULL);
		V_0 = L_4;
		goto IL_0021;
	}

IL_0021:
	{
		bool L_5 = V_0;
		return L_5;
	}
}
IL2CPP_EXTERN_C  bool ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F_AdjustorThunk (RuntimeObject* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_processorInstance, const RuntimeMethod* method)
{
	ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243*>(__this + _offset);
	bool _returnValue;
	_returnValue = ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F(_thisAdjusted, ___0_processorInstance, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249 (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_generatorInstance, const RuntimeMethod* method) 
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_0 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___m_Handle);
		Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(L_0, NULL);
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* L_1 = (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*)(&(&___0_generatorInstance)->___m_ProcessorInstance);
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_2 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&L_1->___Handle);
		Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(L_2, NULL);
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_3 = ___0_generatorInstance;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_4 = L_3.___m_ProcessorInstance;
		ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085* L_5 = L_4.___Header;
		NullCheck(L_5);
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_6 = ((GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0*)L_5)->___Configuration;
		V_0 = L_6;
		goto IL_0032;
	}

IL_0032:
	{
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_7 = V_0;
		return L_7;
	}
}
IL2CPP_EXTERN_C  Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249_AdjustorThunk (RuntimeObject* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_generatorInstance, const RuntimeMethod* method)
{
	ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243*>(__this + _offset);
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 _returnValue;
	_returnValue = ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249(_thisAdjusted, ___0_generatorInstance, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ControlContext_CleanupHeader_mC560997117E3E82A193438753C1020A76A208F5E (ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___0_header, const RuntimeMethod* method) 
{
	bool V_0 = false;
	GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC V_1;
	memset((&V_1), 0, sizeof(V_1));
	bool V_2 = false;
	{
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_0 = ___0_header;
		intptr_t L_1 = L_0->___ManagedTransport;
		bool L_2;
		L_2 = IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline(L_1, 0, NULL);
		V_0 = L_2;
		bool L_3 = V_0;
		if (!L_3)
		{
			goto IL_0038;
		}
	}
	{
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_4 = ___0_header;
		intptr_t L_5 = L_4->___ManagedTransport;
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC L_6;
		L_6 = GCHandle_FromIntPtr_mA7848A4285D007CADC52B6272DB243C8FDFD5FAC(L_5, NULL);
		V_1 = L_6;
		bool L_7;
		L_7 = GCHandle_get_IsAllocated_m241908103D8D867E11CCAB73C918729825E86843_inline((&V_1), NULL);
		V_2 = L_7;
		bool L_8 = V_2;
		if (!L_8)
		{
			goto IL_0037;
		}
	}
	{
		GCHandle_Free_m1320A260E487EB1EA6D95F9E54BFFCB5A4EF83A3((&V_1), NULL);
	}

IL_0037:
	{
	}

IL_0038:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 GeneratorInstance_op_Implicit_mB7A6E46130D3BAB42E00F4DD08155EF286A16167 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* ___0_generatorInstance, const RuntimeMethod* method) 
{
	{
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_0 = ___0_generatorInstance;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_1 = L_0->___m_ProcessorInstance;
		return L_1;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_other, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* L_0 = (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*)(&__this->___m_ProcessorInstance);
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_1 = ___0_other;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_2 = L_1.___m_ProcessorInstance;
		bool L_3;
		L_3 = ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398(L_0, L_2, NULL);
		V_0 = L_3;
		goto IL_0015;
	}

IL_0015:
	{
		bool L_4 = V_0;
		return L_4;
	}
}
IL2CPP_EXTERN_C  bool GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1_AdjustorThunk (RuntimeObject* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_other, const RuntimeMethod* method)
{
	GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*>(__this + _offset);
	bool _returnValue;
	_returnValue = GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GeneratorInstance_Equals_m7412F772B53B5052FF5070FB2BB2099A93270206 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	bool V_1 = false;
	bool V_2 = false;
	int32_t G_B5_0 = 0;
	{
		RuntimeObject* L_0 = ___0_obj;
		V_1 = (bool)((((RuntimeObject*)(RuntimeObject*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_1;
		if (!L_1)
		{
			goto IL_000d;
		}
	}
	{
		V_2 = (bool)0;
		goto IL_0029;
	}

IL_000d:
	{
		RuntimeObject* L_2 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_2, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2_il2cpp_TypeInfo_var)))
		{
			goto IL_0025;
		}
	}
	{
		RuntimeObject* L_3 = ___0_obj;
		V_0 = ((*(GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*)UnBox(L_3, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2_il2cpp_TypeInfo_var)));
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_4 = V_0;
		bool L_5;
		L_5 = GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1(__this, L_4, NULL);
		G_B5_0 = ((int32_t)(L_5));
		goto IL_0026;
	}

IL_0025:
	{
		G_B5_0 = 0;
	}

IL_0026:
	{
		V_2 = (bool)G_B5_0;
		goto IL_0029;
	}

IL_0029:
	{
		bool L_6 = V_2;
		return L_6;
	}
}
IL2CPP_EXTERN_C  bool GeneratorInstance_Equals_m7412F772B53B5052FF5070FB2BB2099A93270206_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*>(__this + _offset);
	bool _returnValue;
	_returnValue = GeneratorInstance_Equals_m7412F772B53B5052FF5070FB2BB2099A93270206(_thisAdjusted, ___0_obj, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t GeneratorInstance_GetHashCode_mBF9BB29BE3D212AAAEF53A02346FED4B0435C7D2 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* L_0 = (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*)(&__this->___m_ProcessorInstance);
		int32_t L_1;
		L_1 = ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0(L_0, NULL);
		V_0 = L_1;
		goto IL_0015;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C  int32_t GeneratorInstance_GetHashCode_mBF9BB29BE3D212AAAEF53A02346FED4B0435C7D2_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*>(__this + _offset);
	int32_t _returnValue;
	_returnValue = GeneratorInstance_GetHashCode_mBF9BB29BE3D212AAAEF53A02346FED4B0435C7D2(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled)
{
	marshaled.___Setup = unmarshaled.___Setup;
	marshaled.___Properties = unmarshaled.___Properties;
	marshaled.___ReportedLength = unmarshaled.___ReportedLength;
	marshaled.___IsFinite = static_cast<int32_t>(unmarshaled.___IsFinite);
	marshaled.___IsRealtime = static_cast<int32_t>(unmarshaled.___IsRealtime);
	marshaled.___HasKnownLength = static_cast<int32_t>(unmarshaled.___HasKnownLength);
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_back(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled)
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 unmarshaledSetup_temp_0;
	memset((&unmarshaledSetup_temp_0), 0, sizeof(unmarshaledSetup_temp_0));
	unmarshaledSetup_temp_0 = marshaled.___Setup;
	unmarshaled.___Setup = unmarshaledSetup_temp_0;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 unmarshaledProperties_temp_1;
	memset((&unmarshaledProperties_temp_1), 0, sizeof(unmarshaledProperties_temp_1));
	unmarshaledProperties_temp_1 = marshaled.___Properties;
	unmarshaled.___Properties = unmarshaledProperties_temp_1;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E unmarshaledReportedLength_temp_2;
	memset((&unmarshaledReportedLength_temp_2), 0, sizeof(unmarshaledReportedLength_temp_2));
	unmarshaledReportedLength_temp_2 = marshaled.___ReportedLength;
	unmarshaled.___ReportedLength = unmarshaledReportedLength_temp_2;
	bool unmarshaledIsFinite_temp_3 = false;
	unmarshaledIsFinite_temp_3 = static_cast<bool>(marshaled.___IsFinite);
	unmarshaled.___IsFinite = unmarshaledIsFinite_temp_3;
	bool unmarshaledIsRealtime_temp_4 = false;
	unmarshaledIsRealtime_temp_4 = static_cast<bool>(marshaled.___IsRealtime);
	unmarshaled.___IsRealtime = unmarshaledIsRealtime_temp_4;
	bool unmarshaledHasKnownLength_temp_5 = false;
	unmarshaledHasKnownLength_temp_5 = static_cast<bool>(marshaled.___HasKnownLength);
	unmarshaled.___HasKnownLength = unmarshaledHasKnownLength_temp_5;
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_cleanup(Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled)
{
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled)
{
	marshaled.___Setup = unmarshaled.___Setup;
	marshaled.___Properties = unmarshaled.___Properties;
	marshaled.___ReportedLength = unmarshaled.___ReportedLength;
	marshaled.___IsFinite = static_cast<int32_t>(unmarshaled.___IsFinite);
	marshaled.___IsRealtime = static_cast<int32_t>(unmarshaled.___IsRealtime);
	marshaled.___HasKnownLength = static_cast<int32_t>(unmarshaled.___HasKnownLength);
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_back(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled)
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 unmarshaledSetup_temp_0;
	memset((&unmarshaledSetup_temp_0), 0, sizeof(unmarshaledSetup_temp_0));
	unmarshaledSetup_temp_0 = marshaled.___Setup;
	unmarshaled.___Setup = unmarshaledSetup_temp_0;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 unmarshaledProperties_temp_1;
	memset((&unmarshaledProperties_temp_1), 0, sizeof(unmarshaledProperties_temp_1));
	unmarshaledProperties_temp_1 = marshaled.___Properties;
	unmarshaled.___Properties = unmarshaledProperties_temp_1;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E unmarshaledReportedLength_temp_2;
	memset((&unmarshaledReportedLength_temp_2), 0, sizeof(unmarshaledReportedLength_temp_2));
	unmarshaledReportedLength_temp_2 = marshaled.___ReportedLength;
	unmarshaled.___ReportedLength = unmarshaledReportedLength_temp_2;
	bool unmarshaledIsFinite_temp_3 = false;
	unmarshaledIsFinite_temp_3 = static_cast<bool>(marshaled.___IsFinite);
	unmarshaled.___IsFinite = unmarshaledIsFinite_temp_3;
	bool unmarshaledIsRealtime_temp_4 = false;
	unmarshaledIsRealtime_temp_4 = static_cast<bool>(marshaled.___IsRealtime);
	unmarshaled.___IsRealtime = unmarshaledIsRealtime_temp_4;
	bool unmarshaledHasKnownLength_temp_5 = false;
	unmarshaledHasKnownLength_temp_5 = static_cast<bool>(marshaled.___HasKnownLength);
	unmarshaled.___HasKnownLength = unmarshaledHasKnownLength_temp_5;
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_cleanup(Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled)
{
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78 (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) 
{
	{
		bool L_0 = __this->___IsRealtime;
		return L_0;
	}
}
IL2CPP_EXTERN_C  bool Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060*>(__this + _offset);
	bool _returnValue;
	_returnValue = Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		bool L_0 = __this->___HasKnownLength;
		if (L_0)
		{
			goto IL_0013;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363));
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_1 = V_0;
		G_B3_0 = L_1;
		goto IL_001e;
	}

IL_0013:
	{
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_2 = __this->___ReportedLength;
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_3;
		memset((&L_3), 0, sizeof(L_3));
		Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604((&L_3), L_2, Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_RuntimeMethod_var);
		G_B3_0 = L_3;
	}

IL_001e:
	{
		return G_B3_0;
	}
}
IL2CPP_EXTERN_C  Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060*>(__this + _offset);
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 _returnValue;
	_returnValue = Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif


IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_pinvoke(const GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0& unmarshaled, GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_pinvoke& marshaled)
{
	marshaled.___Processor = unmarshaled.___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke(unmarshaled.___Configuration, marshaled.___Configuration);
}
IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_pinvoke_back(const GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_pinvoke& marshaled, GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0& unmarshaled)
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 unmarshaledProcessor_temp_0;
	memset((&unmarshaledProcessor_temp_0), 0, sizeof(unmarshaledProcessor_temp_0));
	unmarshaledProcessor_temp_0 = marshaled.___Processor;
	unmarshaled.___Processor = unmarshaledProcessor_temp_0;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 unmarshaledConfiguration_temp_1;
	memset((&unmarshaledConfiguration_temp_1), 0, sizeof(unmarshaledConfiguration_temp_1));
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_back(marshaled.___Configuration, unmarshaledConfiguration_temp_1);
	unmarshaled.___Configuration = unmarshaledConfiguration_temp_1;
}
IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_pinvoke_cleanup(GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_pinvoke& marshaled)
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_cleanup(marshaled.___Configuration);
}


IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_com(const GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0& unmarshaled, GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_com& marshaled)
{
	marshaled.___Processor = unmarshaled.___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com(unmarshaled.___Configuration, marshaled.___Configuration);
}
IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_com_back(const GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_com& marshaled, GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0& unmarshaled)
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 unmarshaledProcessor_temp_0;
	memset((&unmarshaledProcessor_temp_0), 0, sizeof(unmarshaledProcessor_temp_0));
	unmarshaledProcessor_temp_0 = marshaled.___Processor;
	unmarshaled.___Processor = unmarshaledProcessor_temp_0;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 unmarshaledConfiguration_temp_1;
	memset((&unmarshaledConfiguration_temp_1), 0, sizeof(unmarshaledConfiguration_temp_1));
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_back(marshaled.___Configuration, unmarshaledConfiguration_temp_1);
	unmarshaled.___Configuration = unmarshaledConfiguration_temp_1;
}
IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_com_cleanup(GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_com& marshaled)
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_cleanup(marshaled.___Configuration);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398 (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_other, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_0 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___Handle);
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_1 = ___0_other;
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_2 = L_1.___Handle;
		bool L_3;
		L_3 = Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64(L_0, L_2, NULL);
		V_0 = L_3;
		goto IL_0015;
	}

IL_0015:
	{
		bool L_4 = V_0;
		return L_4;
	}
}
IL2CPP_EXTERN_C  bool ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398_AdjustorThunk (RuntimeObject* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_other, const RuntimeMethod* method)
{
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*>(__this + _offset);
	bool _returnValue;
	_returnValue = ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ProcessorInstance_Equals_mFE0F0DA5B311AD6D34EABD73F61C4AE3014084ED (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 V_0;
	memset((&V_0), 0, sizeof(V_0));
	bool V_1 = false;
	bool V_2 = false;
	int32_t G_B5_0 = 0;
	{
		RuntimeObject* L_0 = ___0_obj;
		V_1 = (bool)((((RuntimeObject*)(RuntimeObject*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_1;
		if (!L_1)
		{
			goto IL_000d;
		}
	}
	{
		V_2 = (bool)0;
		goto IL_0029;
	}

IL_000d:
	{
		RuntimeObject* L_2 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_2, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35_il2cpp_TypeInfo_var)))
		{
			goto IL_0025;
		}
	}
	{
		RuntimeObject* L_3 = ___0_obj;
		V_0 = ((*(ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*)UnBox(L_3, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35_il2cpp_TypeInfo_var)));
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_4 = V_0;
		bool L_5;
		L_5 = ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398(__this, L_4, NULL);
		G_B5_0 = ((int32_t)(L_5));
		goto IL_0026;
	}

IL_0025:
	{
		G_B5_0 = 0;
	}

IL_0026:
	{
		V_2 = (bool)G_B5_0;
		goto IL_0029;
	}

IL_0029:
	{
		bool L_6 = V_2;
		return L_6;
	}
}
IL2CPP_EXTERN_C  bool ProcessorInstance_Equals_mFE0F0DA5B311AD6D34EABD73F61C4AE3014084ED_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*>(__this + _offset);
	bool _returnValue;
	_returnValue = ProcessorInstance_Equals_mFE0F0DA5B311AD6D34EABD73F61C4AE3014084ED(_thisAdjusted, ___0_obj, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0 (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_0 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___Handle);
		int32_t L_1;
		L_1 = Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE(L_0, NULL);
		V_0 = L_1;
		goto IL_0015;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C  int32_t ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*>(__this + _offset);
	int32_t _returnValue;
	_returnValue = ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ScriptableGeneratorBindings_InstantiateGeneratorFromObject_m627DB7836271F2551E83B21A49E39952BA58D7B3 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_generatorObjectDefinition, ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___1_control, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* ___2_runtimeHandle, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0D4DFFEDA70FA6D865B68C20C1F40F10FC31B6DD);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5A10D0C56FEA8FB8816E3E1F6F88D55578F844F7);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5C2C12AF4EFFBF824B747CFFBFC316CAE24077D1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC767BA736F2BEEB0EC1BA27ABE77214EC763C9B6);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralEE560D196E71D95F90A39910549B06428576F27C);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	bool V_1 = false;
	ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* V_2 = NULL;
	ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* V_3 = NULL;
	ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243 V_4;
	memset((&V_4), 0, sizeof(V_4));
	Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5 V_5;
	memset((&V_5), 0, sizeof(V_5));
	CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 V_6;
	memset((&V_6), 0, sizeof(V_6));
	bool V_7 = false;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 V_8;
	memset((&V_8), 0, sizeof(V_8));
	bool V_9 = false;
	bool V_10 = false;
	bool V_11 = false;
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 V_12;
	memset((&V_12), 0, sizeof(V_12));
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 V_13;
	memset((&V_13), 0, sizeof(V_13));
	int32_t G_B11_0 = 0;
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___0_generatorObjectDefinition;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_0, IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA_il2cpp_TypeInfo_var));
		RuntimeObject* L_1 = V_0;
		V_1 = (bool)((!(((RuntimeObject*)(RuntimeObject*)L_1) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_2 = V_1;
		if (!L_2)
		{
			goto IL_0169;
		}
	}
	{
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_3 = ___1_control;
		V_3 = L_3;
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_4 = V_3;
		V_2 = (ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67*)((uintptr_t)L_4);
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_5 = V_2;
		ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6((&V_4), (void*)L_5, NULL);
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_6 = ___2_runtimeHandle;
		RuntimeObject* L_7 = V_0;
		ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243 L_8 = V_4;
		il2cpp_codegen_initobj((&V_5), sizeof(Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5));
		Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5 L_9 = V_5;
		il2cpp_codegen_initobj((&V_6), sizeof(CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7));
		CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 L_10 = V_6;
		NullCheck(L_7);
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_11;
		L_11 = InterfaceFuncInvoker3< GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2, ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243, Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5, CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 >::Invoke(0, IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA_il2cpp_TypeInfo_var, L_7, L_8, L_9, L_10);
		*(GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*)L_6 = L_11;
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_12 = ___2_runtimeHandle;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_13;
		L_13 = GeneratorInstance_op_Implicit_mB7A6E46130D3BAB42E00F4DD08155EF286A16167_inline(L_12, NULL);
		bool L_14;
		L_14 = ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F((&V_4), L_13, NULL);
		V_7 = L_14;
		bool L_15 = V_7;
		if (!L_15)
		{
			goto IL_0162;
		}
	}
	{
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_16 = ___2_runtimeHandle;
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_17 = (*(GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*)L_16);
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_18;
		L_18 = ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249((&V_4), L_17, NULL);
		V_8 = L_18;
		RuntimeObject* L_19 = V_0;
		NullCheck(L_19);
		bool L_20;
		L_20 = InterfaceFuncInvoker0< bool >::Invoke(0, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_19);
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_21 = V_8;
		bool L_22 = L_21.___IsFinite;
		V_9 = (bool)((((int32_t)((((int32_t)L_20) == ((int32_t)L_22))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_23 = V_9;
		if (!L_23)
		{
			goto IL_00ac;
		}
	}
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_24 = ___0_generatorObjectDefinition;
		RuntimeObject* L_25 = V_0;
		NullCheck(L_25);
		bool L_26;
		L_26 = InterfaceFuncInvoker0< bool >::Invoke(0, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_25);
		bool L_27 = L_26;
		RuntimeObject* L_28 = Box(il2cpp_defaults.boolean_class, &L_27);
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_29 = V_8;
		bool L_30 = L_29.___IsFinite;
		bool L_31 = L_30;
		RuntimeObject* L_32 = Box(il2cpp_defaults.boolean_class, &L_31);
		String_t* L_33;
		L_33 = String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C(_stringLiteral5C2C12AF4EFFBF824B747CFFBFC316CAE24077D1, L_24, L_28, L_32, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_33, NULL);
	}

IL_00ac:
	{
		RuntimeObject* L_34 = V_0;
		NullCheck(L_34);
		bool L_35;
		L_35 = InterfaceFuncInvoker0< bool >::Invoke(1, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_34);
		bool L_36;
		L_36 = Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline((&V_8), NULL);
		V_10 = (bool)((((int32_t)((((int32_t)L_35) == ((int32_t)L_36))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_37 = V_10;
		if (!L_37)
		{
			goto IL_00ee;
		}
	}
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_38 = ___0_generatorObjectDefinition;
		RuntimeObject* L_39 = V_0;
		NullCheck(L_39);
		bool L_40;
		L_40 = InterfaceFuncInvoker0< bool >::Invoke(1, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_39);
		bool L_41 = L_40;
		RuntimeObject* L_42 = Box(il2cpp_defaults.boolean_class, &L_41);
		bool L_43;
		L_43 = Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline((&V_8), NULL);
		bool L_44 = L_43;
		RuntimeObject* L_45 = Box(il2cpp_defaults.boolean_class, &L_44);
		String_t* L_46;
		L_46 = String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C(_stringLiteral0D4DFFEDA70FA6D865B68C20C1F40F10FC31B6DD, L_38, L_42, L_45, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_46, NULL);
	}

IL_00ee:
	{
		RuntimeObject* L_47 = V_0;
		NullCheck(L_47);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_48;
		L_48 = InterfaceFuncInvoker0< Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 >::Invoke(2, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_47);
		V_12 = L_48;
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_49;
		L_49 = Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B((&V_8), NULL);
		V_13 = L_49;
		bool L_50;
		L_50 = Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_inline((&V_12), Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var);
		bool L_51;
		L_51 = Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_inline((&V_13), Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var);
		if ((((int32_t)L_50) == ((int32_t)L_51)))
		{
			goto IL_0112;
		}
	}
	{
		G_B11_0 = 1;
		goto IL_0131;
	}

IL_0112:
	{
		bool L_52;
		L_52 = Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_inline((&V_12), Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var);
		if (L_52)
		{
			goto IL_011e;
		}
	}
	{
		G_B11_0 = 0;
		goto IL_0131;
	}

IL_011e:
	{
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_53;
		L_53 = Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_inline((&V_12), Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_RuntimeMethod_var);
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_54;
		L_54 = Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_inline((&V_13), Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_RuntimeMethod_var);
		il2cpp_codegen_runtime_class_init_inline(DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E_il2cpp_TypeInfo_var);
		bool L_55;
		L_55 = DiscreteTime_op_Inequality_m4C12F6A54E5C6E8BAC45A3903E6C2A3127C6063C_inline(L_53, L_54, NULL);
		G_B11_0 = ((int32_t)(L_55));
	}

IL_0131:
	{
		V_11 = (bool)G_B11_0;
		bool L_56 = V_11;
		if (!L_56)
		{
			goto IL_0161;
		}
	}
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_57 = ___0_generatorObjectDefinition;
		RuntimeObject* L_58 = V_0;
		NullCheck(L_58);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_59;
		L_59 = InterfaceFuncInvoker0< Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 >::Invoke(2, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_58);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_60 = L_59;
		RuntimeObject* L_61 = Box(Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363_il2cpp_TypeInfo_var, &L_60);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_62;
		L_62 = Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B((&V_8), NULL);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_63 = L_62;
		RuntimeObject* L_64 = Box(Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363_il2cpp_TypeInfo_var, &L_63);
		String_t* L_65;
		L_65 = String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C(_stringLiteralEE560D196E71D95F90A39910549B06428576F27C, L_57, L_61, L_64, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_65, NULL);
	}

IL_0161:
	{
	}

IL_0162:
	{
		V_3 = (ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67*)((uintptr_t)0);
		goto IL_0188;
	}

IL_0169:
	{
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_66 = ___2_runtimeHandle;
		il2cpp_codegen_initobj(L_66, sizeof(GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2));
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_67 = ___0_generatorObjectDefinition;
		String_t* L_68;
		L_68 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteralC767BA736F2BEEB0EC1BA27ABE77214EC763C9B6, L_67, _stringLiteral5A10D0C56FEA8FB8816E3E1F6F88D55578F844F7, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_68, NULL);
	}

IL_0188:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExists_mCA17BD1A2B9E776034C7B312CA810B3B739AA04E (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_handle, ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___1_control, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_0 = ___0_handle;
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_1 = ___1_control;
		bool L_2;
		L_2 = ScriptableProcessorBindings_CheckProcessorExistsInternal_mF6C33BA9D06B46EAAA47D05E74BA20CC604141A6(L_0, (void*)L_1, NULL);
		V_0 = L_2;
		goto IL_000b;
	}

IL_000b:
	{
		bool L_3 = V_0;
		return L_3;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExistsInternal_mF6C33BA9D06B46EAAA47D05E74BA20CC604141A6 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_handle, void* ___1_control, const RuntimeMethod* method) 
{
	{
		void* L_0 = ___1_control;
		bool L_1;
		L_1 = ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831((&___0_handle), L_0, NULL);
		return L_1;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* ___0_handle, void* ___1_control, const RuntimeMethod* method) 
{
	typedef bool (*ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831_ftn) (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*, void*);
	static ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.ScriptableProcessorBindings::CheckProcessorExistsInternal_Injected(Unity.Audio.Handle&,System.Void*)");
	bool icallRetVal = _il2cpp_icall_func(___0_handle, ___1_control);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioResource__ctor_m67FD564B101E07B1EA22EF27F3B5FA290A9D1409 (AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 AudioClipPlayable_Create_m0B42D1553D2752E7E98D10677B2A7DE8CE4DEFD8 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E ___0_graph, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, bool ___2_looping, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableExtensions_SetDuration_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m67FC8BE5E310D54C91DB1D492F7393237401332E_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 V_1;
	memset((&V_1), 0, sizeof(V_1));
	bool V_2 = false;
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 V_3;
	memset((&V_3), 0, sizeof(V_3));
	{
		PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E L_0 = ___0_graph;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_1 = ___1_clip;
		bool L_2 = ___2_looping;
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_3;
		L_3 = AudioClipPlayable_CreateHandle_mD7CA217BC6DE899D586272C372456EBB7ED2A825(L_0, L_1, L_2, NULL);
		V_0 = L_3;
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_4 = V_0;
		AudioClipPlayable__ctor_m4E686B92CFA7C6A36AA794B1E8C36B1E60605751((&V_1), L_4, NULL);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_5 = ___1_clip;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_6;
		L_6 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_5, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		V_2 = L_6;
		bool L_7 = V_2;
		if (!L_7)
		{
			goto IL_002b;
		}
	}
	{
		AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 L_8 = V_1;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_9 = ___1_clip;
		NullCheck(L_9);
		float L_10;
		L_10 = AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D(L_9, NULL);
		PlayableExtensions_SetDuration_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m67FC8BE5E310D54C91DB1D492F7393237401332E(L_8, ((double)L_10), PlayableExtensions_SetDuration_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m67FC8BE5E310D54C91DB1D492F7393237401332E_RuntimeMethod_var);
	}

IL_002b:
	{
		AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 L_11 = V_1;
		V_3 = L_11;
		goto IL_002f;
	}

IL_002f:
	{
		AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 L_12 = V_3;
		return L_12;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_CreateHandle_mD7CA217BC6DE899D586272C372456EBB7ED2A825 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E ___0_graph, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, bool ___2_looping, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	bool V_1 = false;
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = PlayableHandle_get_Null_mF44FE0A71C67054D272F454C91F7E08CBF14A975(NULL);
		V_0 = L_0;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_1 = ___1_clip;
		bool L_2 = ___2_looping;
		bool L_3;
		L_3 = AudioClipPlayable_InternalCreateAudioClipPlayable_mBDAA54F35207F6C62F87CAE268732072C7287616((&___0_graph), L_1, L_2, (&V_0), NULL);
		V_1 = (bool)((((int32_t)L_3) == ((int32_t)0))? 1 : 0);
		bool L_4 = V_1;
		if (!L_4)
		{
			goto IL_0021;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_5;
		L_5 = PlayableHandle_get_Null_mF44FE0A71C67054D272F454C91F7E08CBF14A975(NULL);
		V_2 = L_5;
		goto IL_0025;
	}

IL_0021:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_6 = V_0;
		V_2 = L_6;
		goto IL_0025;
	}

IL_0025:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_7 = V_2;
		return L_7;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable__ctor_m4E686B92CFA7C6A36AA794B1E8C36B1E60605751 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_handle, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_IsPlayableOfType_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m95809D16DD3494AEB0E9E652B42E0CE3167BEE0E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = PlayableHandle_IsValid_m07631D12846BAAF2CC302E69A28A44BFE9EB5098((&___0_handle), NULL);
		V_0 = L_0;
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0027;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = PlayableHandle_IsPlayableOfType_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m95809D16DD3494AEB0E9E652B42E0CE3167BEE0E((&___0_handle), PlayableHandle_IsPlayableOfType_TisAudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0_m95809D16DD3494AEB0E9E652B42E0CE3167BEE0E_RuntimeMethod_var);
		V_1 = (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
		bool L_3 = V_1;
		if (!L_3)
		{
			goto IL_0026;
		}
	}
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_4 = (InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)));
		InvalidCastException__ctor_mED3F9781E6CA47A92C602C896EDDF2F40ECB8644(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral57186E11345D0E246E80500DE17B8C941EF8A38F)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClipPlayable__ctor_m4E686B92CFA7C6A36AA794B1E8C36B1E60605751_RuntimeMethod_var)));
	}

IL_0026:
	{
	}

IL_0027:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_5 = ___0_handle;
		__this->___m_Handle = L_5;
		return;
	}
}
IL2CPP_EXTERN_C  void AudioClipPlayable__ctor_m4E686B92CFA7C6A36AA794B1E8C36B1E60605751_AdjustorThunk (RuntimeObject* __this, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_handle, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	AudioClipPlayable__ctor_m4E686B92CFA7C6A36AA794B1E8C36B1E60605751(_thisAdjusted, ___0_handle, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, const RuntimeMethod* method) 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0 = __this->___m_Handle;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_EXTERN_C  PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 _returnValue;
	_returnValue = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F AudioClipPlayable_op_Implicit_m49C31C8D61799E247CA509124B3E6E8F4009D237 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_playable, const RuntimeMethod* method) 
{
	Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44((&___0_playable), NULL);
		Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F L_1;
		memset((&L_1), 0, sizeof(L_1));
		Playable__ctor_mD2EB35E024816AEED68795D0124EAB30E05BF6C4((&L_1), L_0, NULL);
		V_0 = L_1;
		goto IL_0010;
	}

IL_0010:
	{
		Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 AudioClipPlayable_op_Explicit_mA9B643768436960809DB09A469A0DF09B716446B (Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F ___0_playable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		il2cpp_codegen_runtime_class_init_inline(Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_il2cpp_TypeInfo_var);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = Playable_GetHandle_m39356D23E849DC5428B262092657662C064E04F8((&___0_playable), NULL);
		AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 L_1;
		memset((&L_1), 0, sizeof(L_1));
		AudioClipPlayable__ctor_m4E686B92CFA7C6A36AA794B1E8C36B1E60605751((&L_1), L_0, NULL);
		V_0 = L_1;
		goto IL_0010;
	}

IL_0010:
	{
		AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_other, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44(__this, NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1;
		L_1 = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44((&___0_other), NULL);
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = PlayableHandle_op_Equality_m0E6C48A28F75A870AC22ADE3BD42F7F70A43C99C(L_0, L_1, NULL);
		V_0 = L_2;
		goto IL_0016;
	}

IL_0016:
	{
		bool L_3 = V_0;
		return L_3;
	}
}
IL2CPP_EXTERN_C  bool AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83_AdjustorThunk (RuntimeObject* __this, AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_other, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	bool _returnValue;
	_returnValue = AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetVolume_m3553EC43CBA43CA7802292710A1A284A6DEF8FCC (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, float ___0_value, const RuntimeMethod* method) 
{
	bool V_0 = false;
	int32_t G_B3_0 = 0;
	{
		float L_0 = ___0_value;
		if ((((float)L_0) < ((float)(0.0f))))
		{
			goto IL_0013;
		}
	}
	{
		float L_1 = ___0_value;
		G_B3_0 = ((((float)L_1) > ((float)(1.0f)))? 1 : 0);
		goto IL_0014;
	}

IL_0013:
	{
		G_B3_0 = 1;
	}

IL_0014:
	{
		V_0 = (bool)G_B3_0;
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_002f;
		}
	}
	{
		String_t* L_3;
		L_3 = Single_ToString_mE282EDA9CA4F7DF88432D807732837A629D04972((&___0_value), NULL);
		String_t* L_4;
		L_4 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral0574E31B2E15797E57C51DEC554AE14E00CCB9CC)), L_3, NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_5 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_5, L_4, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClipPlayable_SetVolume_m3553EC43CBA43CA7802292710A1A284A6DEF8FCC_RuntimeMethod_var)));
	}

IL_002f:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_6 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		float L_7 = ___0_value;
		AudioClipPlayable_SetVolumeInternal_m71EFEE49486AACBE4F9C936F4898448AB071B55F(L_6, L_7, NULL);
		return;
	}
}
IL2CPP_EXTERN_C  void AudioClipPlayable_SetVolume_m3553EC43CBA43CA7802292710A1A284A6DEF8FCC_AdjustorThunk (RuntimeObject* __this, float ___0_value, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	AudioClipPlayable_SetVolume_m3553EC43CBA43CA7802292710A1A284A6DEF8FCC(_thisAdjusted, ___0_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetStereoPan_mB8403E93B06B2ECCD0D4D6B0D55C151D0CCCC494 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, float ___0_value, const RuntimeMethod* method) 
{
	bool V_0 = false;
	int32_t G_B3_0 = 0;
	{
		float L_0 = ___0_value;
		if ((((float)L_0) < ((float)(-1.0f))))
		{
			goto IL_0013;
		}
	}
	{
		float L_1 = ___0_value;
		G_B3_0 = ((((float)L_1) > ((float)(1.0f)))? 1 : 0);
		goto IL_0014;
	}

IL_0013:
	{
		G_B3_0 = 1;
	}

IL_0014:
	{
		V_0 = (bool)G_B3_0;
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_002f;
		}
	}
	{
		String_t* L_3;
		L_3 = Single_ToString_mE282EDA9CA4F7DF88432D807732837A629D04972((&___0_value), NULL);
		String_t* L_4;
		L_4 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral0FC2A6DFBAD242B7CE1A74ACA9D3224CE2E0163D)), L_3, NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_5 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_5, L_4, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClipPlayable_SetStereoPan_mB8403E93B06B2ECCD0D4D6B0D55C151D0CCCC494_RuntimeMethod_var)));
	}

IL_002f:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_6 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		float L_7 = ___0_value;
		AudioClipPlayable_SetStereoPanInternal_m206A1B777709E25F42C9EF0BEAF3A84D622D4A90(L_6, L_7, NULL);
		return;
	}
}
IL2CPP_EXTERN_C  void AudioClipPlayable_SetStereoPan_mB8403E93B06B2ECCD0D4D6B0D55C151D0CCCC494_AdjustorThunk (RuntimeObject* __this, float ___0_value, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	AudioClipPlayable_SetStereoPan_mB8403E93B06B2ECCD0D4D6B0D55C151D0CCCC494(_thisAdjusted, ___0_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetSpatialBlend_m72F1B86E4E5940E8587C1EA32BB48463A00810BB (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, float ___0_value, const RuntimeMethod* method) 
{
	bool V_0 = false;
	int32_t G_B3_0 = 0;
	{
		float L_0 = ___0_value;
		if ((((float)L_0) < ((float)(0.0f))))
		{
			goto IL_0013;
		}
	}
	{
		float L_1 = ___0_value;
		G_B3_0 = ((((float)L_1) > ((float)(1.0f)))? 1 : 0);
		goto IL_0014;
	}

IL_0013:
	{
		G_B3_0 = 1;
	}

IL_0014:
	{
		V_0 = (bool)G_B3_0;
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_002f;
		}
	}
	{
		String_t* L_3;
		L_3 = Single_ToString_mE282EDA9CA4F7DF88432D807732837A629D04972((&___0_value), NULL);
		String_t* L_4;
		L_4 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral706336A4F6F53F903F6BD416474754F528D2A13B)), L_3, NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_5 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_5, L_4, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClipPlayable_SetSpatialBlend_m72F1B86E4E5940E8587C1EA32BB48463A00810BB_RuntimeMethod_var)));
	}

IL_002f:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_6 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		float L_7 = ___0_value;
		AudioClipPlayable_SetSpatialBlendInternal_mBAE4A56ACEE90D4732C5D2C5D2D721C65B3DD55B(L_6, L_7, NULL);
		return;
	}
}
IL2CPP_EXTERN_C  void AudioClipPlayable_SetSpatialBlend_m72F1B86E4E5940E8587C1EA32BB48463A00810BB_AdjustorThunk (RuntimeObject* __this, float ___0_value, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	AudioClipPlayable_SetSpatialBlend_m72F1B86E4E5940E8587C1EA32BB48463A00810BB(_thisAdjusted, ___0_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_Seek_m2AB327B535C677793BBADEA634B7AAC9442B1391 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, double ___0_startTime, double ___1_startDelay, double ___2_duration, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	double V_1 = 0.0;
	bool V_2 = false;
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_0 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		double L_1 = ___1_startDelay;
		AudioClipPlayable_SetStartDelayInternal_m1A3816547BF61B7448654300EF078D464A5618CD(L_0, L_1, NULL);
		double L_2 = ___2_duration;
		V_0 = (bool)((((double)L_2) > ((double)(0.0)))? 1 : 0);
		bool L_3 = V_0;
		if (!L_3)
		{
			goto IL_0066;
		}
	}
	{
		double L_4 = ___1_startDelay;
		double L_5 = ___2_duration;
		V_1 = ((double)il2cpp_codegen_add(L_4, L_5));
		double L_6 = V_1;
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_7 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		double L_8;
		L_8 = PlayableHandle_GetDuration_mE3CF9C12CA5A186288A7F1981A92F043100E02A7(L_7, NULL);
		V_2 = (bool)((((int32_t)((!(((double)L_6) >= ((double)L_8)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_9 = V_2;
		if (!L_9)
		{
			goto IL_0045;
		}
	}
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_10 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		PlayableHandle_SetDone_mE04197A8D1657035DC6C70AF7291715557A9FB49(L_10, (bool)1, NULL);
	}

IL_0045:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_11 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		double L_12 = ___2_duration;
		double L_13 = ___0_startTime;
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		PlayableHandle_SetDuration_m4E07F9F3A90B2F3CE10325D9F405993F03CCA08B(L_11, ((double)il2cpp_codegen_add(L_12, L_13)), NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_14 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		double L_15 = ___1_startDelay;
		double L_16 = ___2_duration;
		AudioClipPlayable_SetPauseDelayInternal_m803360EBD0BB692C31D31D620AAB9C8D0CD7A9D1(L_14, ((double)il2cpp_codegen_add(L_15, L_16)), NULL);
		goto IL_009f;
	}

IL_0066:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_17 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		PlayableHandle_SetDone_mE04197A8D1657035DC6C70AF7291715557A9FB49(L_17, (bool)1, NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_18 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		PlayableHandle_SetDuration_m4E07F9F3A90B2F3CE10325D9F405993F03CCA08B(L_18, (1.7976931348623157E+308), NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_19 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		AudioClipPlayable_SetPauseDelayInternal_m803360EBD0BB692C31D31D620AAB9C8D0CD7A9D1(L_19, (0.0), NULL);
	}

IL_009f:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_20 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		double L_21 = ___0_startTime;
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		PlayableHandle_SetTime_m6D255AB6779F3DC278813F1C016FE4EBC9F4B1E4(L_20, L_21, NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_22 = (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*)(&__this->___m_Handle);
		PlayableHandle_Play_m1EF8E1105EB6A4A9B0D6638A57CDCBCEEDC6FB2D(L_22, NULL);
		return;
	}
}
IL2CPP_EXTERN_C  void AudioClipPlayable_Seek_m2AB327B535C677793BBADEA634B7AAC9442B1391_AdjustorThunk (RuntimeObject* __this, double ___0_startTime, double ___1_startDelay, double ___2_duration, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	AudioClipPlayable_Seek_m2AB327B535C677793BBADEA634B7AAC9442B1391(_thisAdjusted, ___0_startTime, ___1_startDelay, ___2_duration, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetVolumeInternal_m71EFEE49486AACBE4F9C936F4898448AB071B55F (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, float ___1_volume, const RuntimeMethod* method) 
{
	typedef void (*AudioClipPlayable_SetVolumeInternal_m71EFEE49486AACBE4F9C936F4898448AB071B55F_ftn) (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*, float);
	static AudioClipPlayable_SetVolumeInternal_m71EFEE49486AACBE4F9C936F4898448AB071B55F_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClipPlayable_SetVolumeInternal_m71EFEE49486AACBE4F9C936F4898448AB071B55F_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioClipPlayable::SetVolumeInternal(UnityEngine.Playables.PlayableHandle&,System.Single)");
	_il2cpp_icall_func(___0_hdl, ___1_volume);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetStereoPanInternal_m206A1B777709E25F42C9EF0BEAF3A84D622D4A90 (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, float ___1_stereoPan, const RuntimeMethod* method) 
{
	typedef void (*AudioClipPlayable_SetStereoPanInternal_m206A1B777709E25F42C9EF0BEAF3A84D622D4A90_ftn) (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*, float);
	static AudioClipPlayable_SetStereoPanInternal_m206A1B777709E25F42C9EF0BEAF3A84D622D4A90_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClipPlayable_SetStereoPanInternal_m206A1B777709E25F42C9EF0BEAF3A84D622D4A90_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioClipPlayable::SetStereoPanInternal(UnityEngine.Playables.PlayableHandle&,System.Single)");
	_il2cpp_icall_func(___0_hdl, ___1_stereoPan);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetSpatialBlendInternal_mBAE4A56ACEE90D4732C5D2C5D2D721C65B3DD55B (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, float ___1_spatialBlend, const RuntimeMethod* method) 
{
	typedef void (*AudioClipPlayable_SetSpatialBlendInternal_mBAE4A56ACEE90D4732C5D2C5D2D721C65B3DD55B_ftn) (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*, float);
	static AudioClipPlayable_SetSpatialBlendInternal_mBAE4A56ACEE90D4732C5D2C5D2D721C65B3DD55B_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClipPlayable_SetSpatialBlendInternal_mBAE4A56ACEE90D4732C5D2C5D2D721C65B3DD55B_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioClipPlayable::SetSpatialBlendInternal(UnityEngine.Playables.PlayableHandle&,System.Single)");
	_il2cpp_icall_func(___0_hdl, ___1_spatialBlend);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetStartDelayInternal_m1A3816547BF61B7448654300EF078D464A5618CD (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, double ___1_delay, const RuntimeMethod* method) 
{
	typedef void (*AudioClipPlayable_SetStartDelayInternal_m1A3816547BF61B7448654300EF078D464A5618CD_ftn) (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*, double);
	static AudioClipPlayable_SetStartDelayInternal_m1A3816547BF61B7448654300EF078D464A5618CD_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClipPlayable_SetStartDelayInternal_m1A3816547BF61B7448654300EF078D464A5618CD_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioClipPlayable::SetStartDelayInternal(UnityEngine.Playables.PlayableHandle&,System.Double)");
	_il2cpp_icall_func(___0_hdl, ___1_delay);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClipPlayable_SetPauseDelayInternal_m803360EBD0BB692C31D31D620AAB9C8D0CD7A9D1 (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_hdl, double ___1_delay, const RuntimeMethod* method) 
{
	typedef void (*AudioClipPlayable_SetPauseDelayInternal_m803360EBD0BB692C31D31D620AAB9C8D0CD7A9D1_ftn) (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*, double);
	static AudioClipPlayable_SetPauseDelayInternal_m803360EBD0BB692C31D31D620AAB9C8D0CD7A9D1_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClipPlayable_SetPauseDelayInternal_m803360EBD0BB692C31D31D620AAB9C8D0CD7A9D1_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioClipPlayable::SetPauseDelayInternal(UnityEngine.Playables.PlayableHandle&,System.Double)");
	_il2cpp_icall_func(___0_hdl, ___1_delay);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_InternalCreateAudioClipPlayable_mBDAA54F35207F6C62F87CAE268732072C7287616 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, bool ___2_looping, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___3_handle, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_Marshal_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m4CF5833467B7EAC854506E5E808202FABD1F15CC_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* L_0 = ___0_graph;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_1 = ___1_clip;
		intptr_t L_2;
		L_2 = MarshalledUnityObject_Marshal_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m4CF5833467B7EAC854506E5E808202FABD1F15CC_inline(L_1, MarshalledUnityObject_Marshal_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m4CF5833467B7EAC854506E5E808202FABD1F15CC_RuntimeMethod_var);
		bool L_3 = ___2_looping;
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* L_4 = ___3_handle;
		bool L_5;
		L_5 = AudioClipPlayable_InternalCreateAudioClipPlayable_Injected_mEA590F72729B23ADA8665AB7E4F022D59126E76B(L_0, L_2, L_3, L_4, NULL);
		return L_5;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_InternalCreateAudioClipPlayable_Injected_mEA590F72729B23ADA8665AB7E4F022D59126E76B (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, intptr_t ___1_clip, bool ___2_looping, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___3_handle, const RuntimeMethod* method) 
{
	typedef bool (*AudioClipPlayable_InternalCreateAudioClipPlayable_Injected_mEA590F72729B23ADA8665AB7E4F022D59126E76B_ftn) (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E*, intptr_t, bool, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*);
	static AudioClipPlayable_InternalCreateAudioClipPlayable_Injected_mEA590F72729B23ADA8665AB7E4F022D59126E76B_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClipPlayable_InternalCreateAudioClipPlayable_Injected_mEA590F72729B23ADA8665AB7E4F022D59126E76B_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioClipPlayable::InternalCreateAudioClipPlayable_Injected(UnityEngine.Playables.PlayableGraph&,System.IntPtr,System.Boolean,UnityEngine.Playables.PlayableHandle&)");
	bool icallRetVal = _il2cpp_icall_func(___0_graph, ___1_clip, ___2_looping, ___3_handle);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixer__ctor_m8BB9BFC96DB436EE4CECE0BECECD5DFC7559058D (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6(__this, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* AudioMixer_FindMatchingGroups_m4541BE177FFA0225AF159156ABB5FE3F5F6CF2CF (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, String_t* ___0_subPath, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Il2CppChar* V_1 = NULL;
	ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E V_2;
	memset((&V_2), 0, sizeof(V_2));
	AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* V_3 = NULL;
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	intptr_t G_B4_0;
	memset((&G_B4_0), 0, sizeof(G_B4_0));
	intptr_t G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0044:
			{
				V_1 = (Il2CppChar*)((uintptr_t)0);
				return;
			}
		});
		try
		{
			{
				intptr_t L_0;
				L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_RuntimeMethod_var);
				intptr_t L_1 = L_0;
				if (L_1)
				{
					G_B2_0 = L_1;
					goto IL_000f_1;
				}
				G_B1_0 = L_1;
			}
			{
				ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
				G_B2_0 = G_B1_0;
			}

IL_000f_1:
			{
				String_t* L_2 = ___0_subPath;
				bool L_3;
				L_3 = StringMarshaller_TryMarshalEmptyOrNullString_m615203C511071D59295D889AB136575DFFEA90A6_inline(L_2, (&V_2), NULL);
				if (L_3)
				{
					G_B4_0 = G_B2_0;
					goto IL_0039_1;
				}
				G_B3_0 = G_B2_0;
			}
			{
				String_t* L_4 = ___0_subPath;
				ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_5;
				L_5 = MemoryExtensions_AsSpan_m0EB07912D71097A8B05F586158966837F5C3DB38_inline(L_4, NULL);
				V_0 = L_5;
				Il2CppChar* L_6;
				L_6 = ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638((&V_0), ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_RuntimeMethod_var);
				V_1 = L_6;
				Il2CppChar* L_7 = V_1;
				int32_t L_8;
				L_8 = ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_inline((&V_0), ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_RuntimeMethod_var);
				ManagedSpanWrapper__ctor_mB29647A21BB87EA4DF859E5C2FA2207F47E525D2((&V_2), (void*)((uintptr_t)L_7), L_8, NULL);
				G_B4_0 = G_B3_0;
			}

IL_0039_1:
			{
				AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* L_9;
				L_9 = AudioMixer_FindMatchingGroups_Injected_m0D316E90F0D1FC826E857AFE66C69B679DFC9500(G_B4_0, (&V_2), NULL);
				V_3 = L_9;
				goto IL_0048;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0048:
	{
		AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* L_10 = V_3;
		return L_10;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixer_SetFloat_m4789959013BE79E4F84F446405914908ADC3F335 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, String_t* ___0_name, float ___1_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Il2CppChar* V_1 = NULL;
	ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E V_2;
	memset((&V_2), 0, sizeof(V_2));
	bool V_3 = false;
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	intptr_t G_B4_0;
	memset((&G_B4_0), 0, sizeof(G_B4_0));
	intptr_t G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0045:
			{
				V_1 = (Il2CppChar*)((uintptr_t)0);
				return;
			}
		});
		try
		{
			{
				intptr_t L_0;
				L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_RuntimeMethod_var);
				intptr_t L_1 = L_0;
				if (L_1)
				{
					G_B2_0 = L_1;
					goto IL_000f_1;
				}
				G_B1_0 = L_1;
			}
			{
				ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
				G_B2_0 = G_B1_0;
			}

IL_000f_1:
			{
				String_t* L_2 = ___0_name;
				bool L_3;
				L_3 = StringMarshaller_TryMarshalEmptyOrNullString_m615203C511071D59295D889AB136575DFFEA90A6_inline(L_2, (&V_2), NULL);
				if (L_3)
				{
					G_B4_0 = G_B2_0;
					goto IL_0039_1;
				}
				G_B3_0 = G_B2_0;
			}
			{
				String_t* L_4 = ___0_name;
				ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_5;
				L_5 = MemoryExtensions_AsSpan_m0EB07912D71097A8B05F586158966837F5C3DB38_inline(L_4, NULL);
				V_0 = L_5;
				Il2CppChar* L_6;
				L_6 = ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638((&V_0), ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_RuntimeMethod_var);
				V_1 = L_6;
				Il2CppChar* L_7 = V_1;
				int32_t L_8;
				L_8 = ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_inline((&V_0), ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_RuntimeMethod_var);
				ManagedSpanWrapper__ctor_mB29647A21BB87EA4DF859E5C2FA2207F47E525D2((&V_2), (void*)((uintptr_t)L_7), L_8, NULL);
				G_B4_0 = G_B3_0;
			}

IL_0039_1:
			{
				float L_9 = ___1_value;
				bool L_10;
				L_10 = AudioMixer_SetFloat_Injected_m07F1E9A4591C32164F5F72AC357D25A023A0634D(G_B4_0, (&V_2), L_9, NULL);
				V_3 = L_10;
				goto IL_0049;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0049:
	{
		bool L_11 = V_3;
		return L_11;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixer_GetFloat_mAED8D277AD30D0346292555CBF81D8961117AEC9 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, String_t* ___0_name, float* ___1_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Il2CppChar* V_1 = NULL;
	ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E V_2;
	memset((&V_2), 0, sizeof(V_2));
	bool V_3 = false;
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	intptr_t G_B4_0;
	memset((&G_B4_0), 0, sizeof(G_B4_0));
	intptr_t G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0045:
			{
				V_1 = (Il2CppChar*)((uintptr_t)0);
				return;
			}
		});
		try
		{
			{
				intptr_t L_0;
				L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04_mB656C0201F2C969B922D161F4E2D27A8B077C4EC_RuntimeMethod_var);
				intptr_t L_1 = L_0;
				if (L_1)
				{
					G_B2_0 = L_1;
					goto IL_000f_1;
				}
				G_B1_0 = L_1;
			}
			{
				ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
				G_B2_0 = G_B1_0;
			}

IL_000f_1:
			{
				String_t* L_2 = ___0_name;
				bool L_3;
				L_3 = StringMarshaller_TryMarshalEmptyOrNullString_m615203C511071D59295D889AB136575DFFEA90A6_inline(L_2, (&V_2), NULL);
				if (L_3)
				{
					G_B4_0 = G_B2_0;
					goto IL_0039_1;
				}
				G_B3_0 = G_B2_0;
			}
			{
				String_t* L_4 = ___0_name;
				ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_5;
				L_5 = MemoryExtensions_AsSpan_m0EB07912D71097A8B05F586158966837F5C3DB38_inline(L_4, NULL);
				V_0 = L_5;
				Il2CppChar* L_6;
				L_6 = ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638((&V_0), ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_RuntimeMethod_var);
				V_1 = L_6;
				Il2CppChar* L_7 = V_1;
				int32_t L_8;
				L_8 = ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_inline((&V_0), ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_RuntimeMethod_var);
				ManagedSpanWrapper__ctor_mB29647A21BB87EA4DF859E5C2FA2207F47E525D2((&V_2), (void*)((uintptr_t)L_7), L_8, NULL);
				G_B4_0 = G_B3_0;
			}

IL_0039_1:
			{
				float* L_9 = ___1_value;
				bool L_10;
				L_10 = AudioMixer_GetFloat_Injected_m2794EC0D637390AAA8773C47325038AB4ECA8F72(G_B4_0, (&V_2), L_9, NULL);
				V_3 = L_10;
				goto IL_0049;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0049:
	{
		bool L_11 = V_3;
		return L_11;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* AudioMixer_FindMatchingGroups_Injected_m0D316E90F0D1FC826E857AFE66C69B679DFC9500 (intptr_t ___0__unity_self, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_subPath, const RuntimeMethod* method) 
{
	typedef AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* (*AudioMixer_FindMatchingGroups_Injected_m0D316E90F0D1FC826E857AFE66C69B679DFC9500_ftn) (intptr_t, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E*);
	static AudioMixer_FindMatchingGroups_Injected_m0D316E90F0D1FC826E857AFE66C69B679DFC9500_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_FindMatchingGroups_Injected_m0D316E90F0D1FC826E857AFE66C69B679DFC9500_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::FindMatchingGroups_Injected(System.IntPtr,UnityEngine.Bindings.ManagedSpanWrapper&)");
	AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* icallRetVal = _il2cpp_icall_func(___0__unity_self, ___1_subPath);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixer_SetFloat_Injected_m07F1E9A4591C32164F5F72AC357D25A023A0634D (intptr_t ___0__unity_self, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_name, float ___2_value, const RuntimeMethod* method) 
{
	typedef bool (*AudioMixer_SetFloat_Injected_m07F1E9A4591C32164F5F72AC357D25A023A0634D_ftn) (intptr_t, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E*, float);
	static AudioMixer_SetFloat_Injected_m07F1E9A4591C32164F5F72AC357D25A023A0634D_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_SetFloat_Injected_m07F1E9A4591C32164F5F72AC357D25A023A0634D_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::SetFloat_Injected(System.IntPtr,UnityEngine.Bindings.ManagedSpanWrapper&,System.Single)");
	bool icallRetVal = _il2cpp_icall_func(___0__unity_self, ___1_name, ___2_value);
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixer_GetFloat_Injected_m2794EC0D637390AAA8773C47325038AB4ECA8F72 (intptr_t ___0__unity_self, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_name, float* ___2_value, const RuntimeMethod* method) 
{
	typedef bool (*AudioMixer_GetFloat_Injected_m2794EC0D637390AAA8773C47325038AB4ECA8F72_ftn) (intptr_t, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E*, float*);
	static AudioMixer_GetFloat_Injected_m2794EC0D637390AAA8773C47325038AB4ECA8F72_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_GetFloat_Injected_m2794EC0D637390AAA8773C47325038AB4ECA8F72_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::GetFloat_Injected(System.IntPtr,UnityEngine.Bindings.ManagedSpanWrapper&,System.Single&)");
	bool icallRetVal = _il2cpp_icall_func(___0__unity_self, ___1_name, ___2_value);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixerGroup__ctor_m0D3A84EDAC9B01AEC0B07AFB1F5B1807F74B9CB8 (AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C AudioMixerPlayable_Create_m323B71EBE332DAF5B2632BAB657BEA33F5870E71 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E ___0_graph, int32_t ___1_inputCount, bool ___2_normalizeInputVolumes, const RuntimeMethod* method) 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E L_0 = ___0_graph;
		int32_t L_1 = ___1_inputCount;
		bool L_2 = ___2_normalizeInputVolumes;
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_3;
		L_3 = AudioMixerPlayable_CreateHandle_mCA2A6EF1CC490A8E59C8EE2020D3304D66B96852(L_0, L_1, L_2, NULL);
		V_0 = L_3;
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_4 = V_0;
		AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C L_5;
		memset((&L_5), 0, sizeof(L_5));
		AudioMixerPlayable__ctor_m2CDDE33FCE0B3F7D9EB482D76515FBF771285F47((&L_5), L_4, NULL);
		V_1 = L_5;
		goto IL_0013;
	}

IL_0013:
	{
		AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C L_6 = V_1;
		return L_6;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_CreateHandle_mCA2A6EF1CC490A8E59C8EE2020D3304D66B96852 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E ___0_graph, int32_t ___1_inputCount, bool ___2_normalizeInputVolumes, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	bool V_1 = false;
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = PlayableHandle_get_Null_mF44FE0A71C67054D272F454C91F7E08CBF14A975(NULL);
		V_0 = L_0;
		bool L_1 = ___2_normalizeInputVolumes;
		bool L_2;
		L_2 = AudioMixerPlayable_CreateAudioMixerPlayableInternal_mB91DD585A6A2903F01F49719CA0045C8727B5AA9((&___0_graph), L_1, (&V_0), NULL);
		V_1 = (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
		bool L_3 = V_1;
		if (!L_3)
		{
			goto IL_0020;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_4;
		L_4 = PlayableHandle_get_Null_mF44FE0A71C67054D272F454C91F7E08CBF14A975(NULL);
		V_2 = L_4;
		goto IL_002d;
	}

IL_0020:
	{
		int32_t L_5 = ___1_inputCount;
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		PlayableHandle_SetInputCount_m6067CD3616C428F777903FCBFD789060A2185DEE((&V_0), L_5, NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_6 = V_0;
		V_2 = L_6;
		goto IL_002d;
	}

IL_002d:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_7 = V_2;
		return L_7;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixerPlayable__ctor_m2CDDE33FCE0B3F7D9EB482D76515FBF771285F47 (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_handle, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_IsPlayableOfType_TisAudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C_m28F89BE3D7CCE69B28258CC5B1C7D4E814F48CAB_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = PlayableHandle_IsValid_m07631D12846BAAF2CC302E69A28A44BFE9EB5098((&___0_handle), NULL);
		V_0 = L_0;
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0027;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = PlayableHandle_IsPlayableOfType_TisAudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C_m28F89BE3D7CCE69B28258CC5B1C7D4E814F48CAB((&___0_handle), PlayableHandle_IsPlayableOfType_TisAudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C_m28F89BE3D7CCE69B28258CC5B1C7D4E814F48CAB_RuntimeMethod_var);
		V_1 = (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
		bool L_3 = V_1;
		if (!L_3)
		{
			goto IL_0026;
		}
	}
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_4 = (InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)));
		InvalidCastException__ctor_mED3F9781E6CA47A92C602C896EDDF2F40ECB8644(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralC51F74D9F7385459B7E90865166418A267D9A62B)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioMixerPlayable__ctor_m2CDDE33FCE0B3F7D9EB482D76515FBF771285F47_RuntimeMethod_var)));
	}

IL_0026:
	{
	}

IL_0027:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_5 = ___0_handle;
		__this->___m_Handle = L_5;
		return;
	}
}
IL2CPP_EXTERN_C  void AudioMixerPlayable__ctor_m2CDDE33FCE0B3F7D9EB482D76515FBF771285F47_AdjustorThunk (RuntimeObject* __this, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_handle, const RuntimeMethod* method)
{
	AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C*>(__this + _offset);
	AudioMixerPlayable__ctor_m2CDDE33FCE0B3F7D9EB482D76515FBF771285F47(_thisAdjusted, ___0_handle, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, const RuntimeMethod* method) 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0 = __this->___m_Handle;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_EXTERN_C  PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C*>(__this + _offset);
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 _returnValue;
	_returnValue = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F AudioMixerPlayable_op_Implicit_m479542341C4CAEE00B4F7DD0B68E39F8E4388974 (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_playable, const RuntimeMethod* method) 
{
	Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD((&___0_playable), NULL);
		Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F L_1;
		memset((&L_1), 0, sizeof(L_1));
		Playable__ctor_mD2EB35E024816AEED68795D0124EAB30E05BF6C4((&L_1), L_0, NULL);
		V_0 = L_1;
		goto IL_0010;
	}

IL_0010:
	{
		Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57 (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_other, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD(__this, NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1;
		L_1 = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD((&___0_other), NULL);
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = PlayableHandle_op_Equality_m0E6C48A28F75A870AC22ADE3BD42F7F70A43C99C(L_0, L_1, NULL);
		V_0 = L_2;
		goto IL_0016;
	}

IL_0016:
	{
		bool L_3 = V_0;
		return L_3;
	}
}
IL2CPP_EXTERN_C  bool AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57_AdjustorThunk (RuntimeObject* __this, AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_other, const RuntimeMethod* method)
{
	AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C*>(__this + _offset);
	bool _returnValue;
	_returnValue = AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixerPlayable_CreateAudioMixerPlayableInternal_mB91DD585A6A2903F01F49719CA0045C8727B5AA9 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, bool ___1_normalizeInputVolumes, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___2_handle, const RuntimeMethod* method) 
{
	typedef bool (*AudioMixerPlayable_CreateAudioMixerPlayableInternal_mB91DD585A6A2903F01F49719CA0045C8727B5AA9_ftn) (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E*, bool, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4*);
	static AudioMixerPlayable_CreateAudioMixerPlayableInternal_mB91DD585A6A2903F01F49719CA0045C8727B5AA9_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixerPlayable_CreateAudioMixerPlayableInternal_mB91DD585A6A2903F01F49719CA0045C8727B5AA9_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixerPlayable::CreateAudioMixerPlayableInternal(UnityEngine.Playables.PlayableGraph&,System.Boolean,UnityEngine.Playables.PlayableHandle&)");
	bool icallRetVal = _il2cpp_icall_func(___0_graph, ___1_normalizeInputVolumes, ___2_handle);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4 AudioPlayableBinding_Create_m4E506BD0649ADAB9D1C2D924D2767FBB0C63DACB (String_t* ___0_name, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_key, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioPlayableBinding_CreateAudioOutput_m2BE0EDC74ADA082B5CD6247ACD78EFCBDB85ADC3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		String_t* L_0 = ___0_name;
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_1 = ___1_key;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_2 = { reinterpret_cast<intptr_t> (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_0_0_0_var) };
		il2cpp_codegen_runtime_class_init_inline(il2cpp_defaults.systemtype_class);
		Type_t* L_3;
		L_3 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_2, NULL);
		CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509* L_4 = (CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509*)il2cpp_codegen_object_new(CreateOutputMethod_tD18AFE3B69E6DDD913D82D5FA1D5D909CEEC8509_il2cpp_TypeInfo_var);
		CreateOutputMethod__ctor_m5A339017CD8ECB0140EB936FD2A5B589B20166B4(L_4, NULL, (intptr_t)((void*)AudioPlayableBinding_CreateAudioOutput_m2BE0EDC74ADA082B5CD6247ACD78EFCBDB85ADC3_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4_il2cpp_TypeInfo_var);
		PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4 L_5;
		L_5 = PlayableBinding_CreateInternal_m63C8C98E659B52725E0373019ECFCEC22167A47A(L_0, L_1, L_3, L_4, NULL);
		V_0 = L_5;
		goto IL_0021;
	}

IL_0021:
	{
		PlayableBinding_tB68B3BAC47F4F4C559640472174D5BEF93CB6AB4 L_6 = V_0;
		return L_6;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 AudioPlayableBinding_CreateAudioOutput_m2BE0EDC74ADA082B5CD6247ACD78EFCBDB85ADC3 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E ___0_graph, String_t* ___1_name, const RuntimeMethod* method) 
{
	PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E L_0 = ___0_graph;
		String_t* L_1 = ___1_name;
		AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 L_2;
		L_2 = AudioPlayableOutput_Create_m90DF38B28813932D4246094FD4DB6105572619D2(L_0, L_1, (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*)NULL, NULL);
		PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 L_3;
		L_3 = AudioPlayableOutput_op_Implicit_mD2D35763126BDE08E10CA74D8E8C49988477F428(L_2, NULL);
		V_0 = L_3;
		goto IL_0011;
	}

IL_0011:
	{
		PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 L_4 = V_0;
		return L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioPlayableGraphExtensions_InternalCreateAudioOutput_m5EF8B18878AFFE0B462E615C8243C2433A3F5E78 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, String_t* ___1_name, PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___2_handle, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Il2CppChar* V_1 = NULL;
	ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E V_2;
	memset((&V_2), 0, sizeof(V_2));
	bool V_3 = false;
	PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* G_B2_0 = NULL;
	PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* G_B1_0 = NULL;
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0036:
			{
				V_1 = (Il2CppChar*)((uintptr_t)0);
				return;
			}
		});
		try
		{
			{
				PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* L_0 = ___0_graph;
				String_t* L_1 = ___1_name;
				bool L_2;
				L_2 = StringMarshaller_TryMarshalEmptyOrNullString_m615203C511071D59295D889AB136575DFFEA90A6_inline(L_1, (&V_2), NULL);
				if (L_2)
				{
					G_B2_0 = L_0;
					goto IL_002a_1;
				}
				G_B1_0 = L_0;
			}
			{
				String_t* L_3 = ___1_name;
				ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_4;
				L_4 = MemoryExtensions_AsSpan_m0EB07912D71097A8B05F586158966837F5C3DB38_inline(L_3, NULL);
				V_0 = L_4;
				Il2CppChar* L_5;
				L_5 = ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638((&V_0), ReadOnlySpan_1_GetPinnableReference_mB710059C1A1A30270065958DE8345808C6683638_RuntimeMethod_var);
				V_1 = L_5;
				Il2CppChar* L_6 = V_1;
				int32_t L_7;
				L_7 = ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_inline((&V_0), ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_RuntimeMethod_var);
				ManagedSpanWrapper__ctor_mB29647A21BB87EA4DF859E5C2FA2207F47E525D2((&V_2), (void*)((uintptr_t)L_6), L_7, NULL);
				G_B2_0 = G_B1_0;
			}

IL_002a_1:
			{
				PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* L_8 = ___2_handle;
				bool L_9;
				L_9 = AudioPlayableGraphExtensions_InternalCreateAudioOutput_Injected_m4290FFF24DD0F825CC4036371953C9B67865ED3C(G_B2_0, (&V_2), L_8, NULL);
				V_3 = L_9;
				goto IL_003a;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_003a:
	{
		bool L_10 = V_3;
		return L_10;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioPlayableGraphExtensions_InternalCreateAudioOutput_Injected_m4290FFF24DD0F825CC4036371953C9B67865ED3C (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E* ___0_graph, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_name, PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___2_handle, const RuntimeMethod* method) 
{
	typedef bool (*AudioPlayableGraphExtensions_InternalCreateAudioOutput_Injected_m4290FFF24DD0F825CC4036371953C9B67865ED3C_ftn) (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E*, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E*, PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883*);
	static AudioPlayableGraphExtensions_InternalCreateAudioOutput_Injected_m4290FFF24DD0F825CC4036371953C9B67865ED3C_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioPlayableGraphExtensions_InternalCreateAudioOutput_Injected_m4290FFF24DD0F825CC4036371953C9B67865ED3C_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioPlayableGraphExtensions::InternalCreateAudioOutput_Injected(UnityEngine.Playables.PlayableGraph&,UnityEngine.Bindings.ManagedSpanWrapper&,UnityEngine.Playables.PlayableOutputHandle&)");
	bool icallRetVal = _il2cpp_icall_func(___0_graph, ___1_name, ___2_handle);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 AudioPlayableOutput_Create_m90DF38B28813932D4246094FD4DB6105572619D2 (PlayableGraph_t4A5B0B45343A240F0761574FD7C672E0CFFF7A6E ___0_graph, String_t* ___1_name, AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___2_target, const RuntimeMethod* method) 
{
	PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 V_0;
	memset((&V_0), 0, sizeof(V_0));
	AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 V_1;
	memset((&V_1), 0, sizeof(V_1));
	bool V_2 = false;
	AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 V_3;
	memset((&V_3), 0, sizeof(V_3));
	{
		String_t* L_0 = ___1_name;
		bool L_1;
		L_1 = AudioPlayableGraphExtensions_InternalCreateAudioOutput_m5EF8B18878AFFE0B462E615C8243C2433A3F5E78((&___0_graph), L_0, (&V_0), NULL);
		V_2 = (bool)((((int32_t)L_1) == ((int32_t)0))? 1 : 0);
		bool L_2 = V_2;
		if (!L_2)
		{
			goto IL_001a;
		}
	}
	{
		AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 L_3;
		L_3 = AudioPlayableOutput_get_Null_m8404386CE6506C8C0574B74023EB1BC17A45F205(NULL);
		V_3 = L_3;
		goto IL_002f;
	}

IL_001a:
	{
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 L_4 = V_0;
		AudioPlayableOutput__ctor_mBEA3D7EE652908558720EDC0D40F7BF3EC50D409((&V_1), L_4, NULL);
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_5 = ___2_target;
		AudioPlayableOutput_SetTarget_m34EE86E5C2833F12993681ABE5AC85961836DE04((&V_1), L_5, NULL);
		AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 L_6 = V_1;
		V_3 = L_6;
		goto IL_002f;
	}

IL_002f:
	{
		AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 L_7 = V_3;
		return L_7;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput__ctor_mBEA3D7EE652908558720EDC0D40F7BF3EC50D409 (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* __this, PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 ___0_handle, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableOutputHandle_IsPlayableOutputOfType_TisAudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20_mAC4BC6AF8D183B1A972EA239A6D9BEA34054C7C6_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = PlayableOutputHandle_IsValid_mA94C343D72B72C3B1C7636C93A8DB5EA71691312((&___0_handle), NULL);
		V_0 = L_0;
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0027;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = PlayableOutputHandle_IsPlayableOutputOfType_TisAudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20_mAC4BC6AF8D183B1A972EA239A6D9BEA34054C7C6((&___0_handle), PlayableOutputHandle_IsPlayableOutputOfType_TisAudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20_mAC4BC6AF8D183B1A972EA239A6D9BEA34054C7C6_RuntimeMethod_var);
		V_1 = (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
		bool L_3 = V_1;
		if (!L_3)
		{
			goto IL_0026;
		}
	}
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_4 = (InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)));
		InvalidCastException__ctor_mED3F9781E6CA47A92C602C896EDDF2F40ECB8644(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD7EA9435CDE6F5B13E32FE646D502752ECCD43DD)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioPlayableOutput__ctor_mBEA3D7EE652908558720EDC0D40F7BF3EC50D409_RuntimeMethod_var)));
	}

IL_0026:
	{
	}

IL_0027:
	{
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 L_5 = ___0_handle;
		__this->___m_Handle = L_5;
		return;
	}
}
IL2CPP_EXTERN_C  void AudioPlayableOutput__ctor_mBEA3D7EE652908558720EDC0D40F7BF3EC50D409_AdjustorThunk (RuntimeObject* __this, PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 ___0_handle, const RuntimeMethod* method)
{
	AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20*>(__this + _offset);
	AudioPlayableOutput__ctor_mBEA3D7EE652908558720EDC0D40F7BF3EC50D409(_thisAdjusted, ___0_handle, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 AudioPlayableOutput_get_Null_m8404386CE6506C8C0574B74023EB1BC17A45F205 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883_il2cpp_TypeInfo_var);
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 L_0;
		L_0 = PlayableOutputHandle_get_Null_m656E8D2549FA031DA8A2EA5B39CE3B33D75B69F8(NULL);
		AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 L_1;
		memset((&L_1), 0, sizeof(L_1));
		AudioPlayableOutput__ctor_mBEA3D7EE652908558720EDC0D40F7BF3EC50D409((&L_1), L_0, NULL);
		V_0 = L_1;
		goto IL_000e;
	}

IL_000e:
	{
		AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 AudioPlayableOutput_GetHandle_m55153D572F8FB9BCFF3843402A20280273B934AE (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* __this, const RuntimeMethod* method) 
{
	PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 L_0 = __this->___m_Handle;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_EXTERN_C  PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 AudioPlayableOutput_GetHandle_m55153D572F8FB9BCFF3843402A20280273B934AE_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20*>(__this + _offset);
	PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 _returnValue;
	_returnValue = AudioPlayableOutput_GetHandle_m55153D572F8FB9BCFF3843402A20280273B934AE(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 AudioPlayableOutput_op_Implicit_mD2D35763126BDE08E10CA74D8E8C49988477F428 (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 ___0_output, const RuntimeMethod* method) 
{
	PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 L_0;
		L_0 = AudioPlayableOutput_GetHandle_m55153D572F8FB9BCFF3843402A20280273B934AE((&___0_output), NULL);
		PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 L_1;
		memset((&L_1), 0, sizeof(L_1));
		PlayableOutput__ctor_m55FBB20EC479F67641835EA48D84A1AB3DF39747((&L_1), L_0, NULL);
		V_0 = L_1;
		goto IL_0010;
	}

IL_0010:
	{
		PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 AudioPlayableOutput_op_Explicit_mC51D8736040715BAA8AC5FA22B6E89F9CDBF25C1 (PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680 ___0_output, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		il2cpp_codegen_runtime_class_init_inline(PlayableOutput_t2F7C45A58DA3E788EEDDB439549E21CF3FCF3680_il2cpp_TypeInfo_var);
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 L_0;
		L_0 = PlayableOutput_GetHandle_m12FF2889D15884CBEB92A6CB376827EBA5A607BF((&___0_output), NULL);
		AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 L_1;
		memset((&L_1), 0, sizeof(L_1));
		AudioPlayableOutput__ctor_mBEA3D7EE652908558720EDC0D40F7BF3EC50D409((&L_1), L_0, NULL);
		V_0 = L_1;
		goto IL_0010;
	}

IL_0010:
	{
		AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_SetTarget_m34EE86E5C2833F12993681ABE5AC85961836DE04 (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* __this, AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_value, const RuntimeMethod* method) 
{
	{
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* L_0 = (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883*)(&__this->___m_Handle);
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_1 = ___0_value;
		AudioPlayableOutput_InternalSetTarget_m3A9912A00BC052FCEDEBB5EB75DCBE5B2B9DA86E(L_0, L_1, NULL);
		return;
	}
}
IL2CPP_EXTERN_C  void AudioPlayableOutput_SetTarget_m34EE86E5C2833F12993681ABE5AC85961836DE04_AdjustorThunk (RuntimeObject* __this, AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_value, const RuntimeMethod* method)
{
	AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20*>(__this + _offset);
	AudioPlayableOutput_SetTarget_m34EE86E5C2833F12993681ABE5AC85961836DE04(_thisAdjusted, ___0_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_SetEvaluateOnSeek_mB3266A8A68E94933A82A0C431B7A2E7321929D92 (AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* L_0 = (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883*)(&__this->___m_Handle);
		bool L_1 = ___0_value;
		AudioPlayableOutput_InternalSetEvaluateOnSeek_m639F527A20B97D2617898E223B5CDC1DC8548804(L_0, L_1, NULL);
		return;
	}
}
IL2CPP_EXTERN_C  void AudioPlayableOutput_SetEvaluateOnSeek_mB3266A8A68E94933A82A0C431B7A2E7321929D92_AdjustorThunk (RuntimeObject* __this, bool ___0_value, const RuntimeMethod* method)
{
	AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20*>(__this + _offset);
	AudioPlayableOutput_SetEvaluateOnSeek_mB3266A8A68E94933A82A0C431B7A2E7321929D92(_thisAdjusted, ___0_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_InternalSetTarget_m3A9912A00BC052FCEDEBB5EB75DCBE5B2B9DA86E (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___0_output, AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___1_target, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_Marshal_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m792F1F32E543E9AE922C6F04B2616F0690E1D1CB_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* L_0 = ___0_output;
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_1 = ___1_target;
		intptr_t L_2;
		L_2 = MarshalledUnityObject_Marshal_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m792F1F32E543E9AE922C6F04B2616F0690E1D1CB_inline(L_1, MarshalledUnityObject_Marshal_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m792F1F32E543E9AE922C6F04B2616F0690E1D1CB_RuntimeMethod_var);
		AudioPlayableOutput_InternalSetTarget_Injected_m56A381E3FFAEA1292A2D4C09ECE08D06FEE25ADC(L_0, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_InternalSetEvaluateOnSeek_m639F527A20B97D2617898E223B5CDC1DC8548804 (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___0_output, bool ___1_value, const RuntimeMethod* method) 
{
	typedef void (*AudioPlayableOutput_InternalSetEvaluateOnSeek_m639F527A20B97D2617898E223B5CDC1DC8548804_ftn) (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883*, bool);
	static AudioPlayableOutput_InternalSetEvaluateOnSeek_m639F527A20B97D2617898E223B5CDC1DC8548804_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioPlayableOutput_InternalSetEvaluateOnSeek_m639F527A20B97D2617898E223B5CDC1DC8548804_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioPlayableOutput::InternalSetEvaluateOnSeek(UnityEngine.Playables.PlayableOutputHandle&,System.Boolean)");
	_il2cpp_icall_func(___0_output, ___1_value);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayableOutput_InternalSetTarget_Injected_m56A381E3FFAEA1292A2D4C09ECE08D06FEE25ADC (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883* ___0_output, intptr_t ___1_target, const RuntimeMethod* method) 
{
	typedef void (*AudioPlayableOutput_InternalSetTarget_Injected_m56A381E3FFAEA1292A2D4C09ECE08D06FEE25ADC_ftn) (PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883*, intptr_t);
	static AudioPlayableOutput_InternalSetTarget_Injected_m56A381E3FFAEA1292A2D4C09ECE08D06FEE25ADC_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioPlayableOutput_InternalSetTarget_Injected_m56A381E3FFAEA1292A2D4C09ECE08D06FEE25ADC_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioPlayableOutput::InternalSetTarget_Injected(UnityEngine.Playables.PlayableOutputHandle&,System.IntPtr)");
	_il2cpp_icall_func(___0_output, ___1_target);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) 
{
	Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* V_0 = NULL;
	{
		intptr_t L_0 = __this->___m_Node;
		void* L_1;
		L_1 = IntPtr_op_Explicit_m2728CBA081E79B97DDCF1D4FAD77B309CA1E94BF(L_0, NULL);
		V_0 = (Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8*)L_1;
		goto IL_000f;
	}

IL_000f:
	{
		Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C  Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* _returnValue;
	_returnValue = Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_other, const RuntimeMethod* method) 
{
	bool V_0 = false;
	int32_t G_B3_0 = 0;
	{
		intptr_t L_0 = __this->___m_Node;
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_1 = ___0_other;
		intptr_t L_2 = L_1.___m_Node;
		bool L_3;
		L_3 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, L_2, NULL);
		if (!L_3)
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_4 = __this->___Version;
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_5 = ___0_other;
		int32_t L_6 = L_5.___Version;
		G_B3_0 = ((((int32_t)L_4) == ((int32_t)L_6))? 1 : 0);
		goto IL_0025;
	}

IL_0024:
	{
		G_B3_0 = 0;
	}

IL_0025:
	{
		V_0 = (bool)G_B3_0;
		goto IL_0028;
	}

IL_0028:
	{
		bool L_7 = V_0;
		return L_7;
	}
}
IL2CPP_EXTERN_C  bool Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64_AdjustorThunk (RuntimeObject* __this, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_other, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	bool _returnValue;
	_returnValue = Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_Equals_mDE217E745B1016A74BCE82848675ED913B391B8F (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	int32_t G_B5_0 = 0;
	{
		RuntimeObject* L_0 = ___0_obj;
		V_0 = (bool)((((RuntimeObject*)(RuntimeObject*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_000d;
		}
	}
	{
		V_1 = (bool)0;
		goto IL_0027;
	}

IL_000d:
	{
		RuntimeObject* L_2 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_2, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153_il2cpp_TypeInfo_var)))
		{
			goto IL_0023;
		}
	}
	{
		RuntimeObject* L_3 = ___0_obj;
		bool L_4;
		L_4 = Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64(__this, ((*(Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)UnBox(L_3, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153_il2cpp_TypeInfo_var))), NULL);
		G_B5_0 = ((int32_t)(L_4));
		goto IL_0024;
	}

IL_0023:
	{
		G_B5_0 = 0;
	}

IL_0024:
	{
		V_1 = (bool)G_B5_0;
		goto IL_0027;
	}

IL_0027:
	{
		bool L_5 = V_1;
		return L_5;
	}
}
IL2CPP_EXTERN_C  bool Handle_Equals_mDE217E745B1016A74BCE82848675ED913B391B8F_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	bool _returnValue;
	_returnValue = Handle_Equals_mDE217E745B1016A74BCE82848675ED913B391B8F(_thisAdjusted, ___0_obj, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		intptr_t L_0 = __this->___m_Node;
		int32_t L_1;
		L_1 = IntPtr_op_Explicit_mC33775570B5AC86421DABA8F8B9885DBFF49B02F(L_0, NULL);
		int32_t L_2 = __this->___Version;
		V_0 = ((int32_t)(((int32_t)il2cpp_codegen_multiply(L_1, ((int32_t)397)))^L_2));
		goto IL_001d;
	}

IL_001d:
	{
		int32_t L_3 = V_0;
		return L_3;
	}
}
IL2CPP_EXTERN_C  int32_t Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	int32_t _returnValue;
	_returnValue = Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		bool L_0;
		L_0 = Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145(__this, NULL);
		V_0 = (bool)((((int32_t)L_0) == ((int32_t)0))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0019;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_2 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral23ABA34B45DF51C7D9C0E116AA6FDA4749E8C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3_RuntimeMethod_var)));
	}

IL_0019:
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) 
{
	int32_t G_B4_0 = 0;
	{
		intptr_t L_0 = __this->___m_Node;
		bool L_1;
		L_1 = IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0035;
		}
	}
	{
		Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* L_2;
		L_2 = Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50(__this, NULL);
		NullCheck(L_2);
		int32_t L_3 = L_2->___Version;
		int32_t L_4 = __this->___Version;
		if ((!(((uint32_t)L_3) == ((uint32_t)L_4))))
		{
			goto IL_0035;
		}
	}
	{
		Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* L_5;
		L_5 = Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50(__this, NULL);
		NullCheck(L_5);
		int32_t L_6 = L_5->___AllocationFlags;
		G_B4_0 = ((((int32_t)L_6) == ((int32_t)0))? 1 : 0);
		goto IL_0036;
	}

IL_0035:
	{
		G_B4_0 = 0;
	}

IL_0036:
	{
		return (bool)G_B4_0;
	}
}
IL2CPP_EXTERN_C  bool Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	bool _returnValue;
	_returnValue = Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_inline (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_deviceWasChanged, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_inline (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_data, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_inline (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_position, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_provider, ___1_sampleFrameCount, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) 
{
	{
		intptr_t L_0 = ___0_value1;
		intptr_t L_1 = ___1_value2;
		return (bool)((((int32_t)((((intptr_t)L_0) == ((intptr_t)L_1))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool GCHandle_get_IsAllocated_m241908103D8D867E11CCAB73C918729825E86843_inline (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) 
{
	{
		intptr_t L_0 = __this->___handle;
		bool L_1;
		L_1 = IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline(L_0, 0, NULL);
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) 
{
	{
		bool L_0 = __this->___IsRealtime;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 GeneratorInstance_op_Implicit_mB7A6E46130D3BAB42E00F4DD08155EF286A16167_inline (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* ___0_generatorInstance, const RuntimeMethod* method) 
{
	{
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_0 = ___0_generatorInstance;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_1 = L_0->___m_ProcessorInstance;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DiscreteTime_op_Inequality_m4C12F6A54E5C6E8BAC45A3903E6C2A3127C6063C_inline (DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___0_lhs, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___1_rhs, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_0 = ___0_lhs;
		int64_t L_1 = L_0.___Value;
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_2 = ___1_rhs;
		int64_t L_3 = L_2.___Value;
		V_0 = (bool)((((int32_t)((((int64_t)L_1) == ((int64_t)L_3))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		goto IL_0015;
	}

IL_0015:
	{
		bool L_4 = V_0;
		return L_4;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool StringMarshaller_TryMarshalEmptyOrNullString_m615203C511071D59295D889AB136575DFFEA90A6_inline (String_t* ___0_s, ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* ___1_managedSpanWrapper, const RuntimeMethod* method) 
{
	bool V_0 = false;
	bool V_1 = false;
	bool V_2 = false;
	{
		String_t* L_0 = ___0_s;
		V_0 = (bool)((((RuntimeObject*)(String_t*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* L_2 = ___1_managedSpanWrapper;
		il2cpp_codegen_initobj(L_2, sizeof(ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E));
		V_1 = (bool)1;
		goto IL_0043;
	}

IL_0015:
	{
		String_t* L_3 = ___0_s;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_3, NULL);
		V_2 = (bool)((((int32_t)L_4) == ((int32_t)0))? 1 : 0);
		bool L_5 = V_2;
		if (!L_5)
		{
			goto IL_003f;
		}
	}
	{
		ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E* L_6 = ___1_managedSpanWrapper;
		uintptr_t L_7;
		L_7 = UIntPtr_op_Explicit_mF1E7911DD5AC13B5E59EE8C7903469D12A3861E8(((int64_t)1), NULL);
		void* L_8;
		L_8 = UIntPtr_op_Explicit_m42C3EA82465934F505B4274A7CE320550A48B7B9(L_7, NULL);
		ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E L_9;
		memset((&L_9), 0, sizeof(L_9));
		ManagedSpanWrapper__ctor_mB29647A21BB87EA4DF859E5C2FA2207F47E525D2((&L_9), L_8, 0, NULL);
		*(ManagedSpanWrapper_tE7FC4BBB631B130757F8DEB15853D98FD3D5DC0E*)L_6 = L_9;
		V_1 = (bool)1;
		goto IL_0043;
	}

IL_003f:
	{
		V_1 = (bool)0;
		goto IL_0043;
	}

IL_0043:
	{
		bool L_10 = V_1;
		return L_10;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 MemoryExtensions_AsSpan_m0EB07912D71097A8B05F586158966837F5C3DB38_inline (String_t* ___0_text, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		String_t* L_0 = ___0_text;
		if (L_0)
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1));
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_1 = V_0;
		return L_1;
	}

IL_000d:
	{
		String_t* L_2 = ___0_text;
		NullCheck(L_2);
		Il2CppChar* L_3;
		L_3 = String_GetRawStringData_m87BC50B7B314C055E27A28032D1003D42FDE411D(L_2, NULL);
		String_t* L_4 = ___0_text;
		NullCheck(L_4);
		int32_t L_5;
		L_5 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_4, NULL);
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_6;
		memset((&L_6), 0, sizeof(L_6));
		ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_inline((&L_6), L_3, L_5, ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_RuntimeMethod_var);
		return L_6;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) 
{
	{
		intptr_t L_0 = ___0_value1;
		intptr_t L_1 = ___1_value2;
		return (bool)((((intptr_t)L_0) == ((intptr_t)L_1))? 1 : 0);
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline (RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_obj;
		NullCheck(L_0);
		intptr_t L_1 = ((Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)L_0)->___m_CachedPtr;
		V_0 = L_1;
		goto IL_000f;
	}

IL_000f:
	{
		intptr_t L_2 = V_0;
		return L_2;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Unmarshal_UnmarshalUnityObject_TisRuntimeObject_m2B04FEB8E4BB254DD6F827F3111C91F2AF16D04A_gshared_inline (intptr_t ___0_gcHandlePtr, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC V_0;
	memset((&V_0), 0, sizeof(V_0));
	RuntimeObject* V_1 = NULL;
	bool V_2 = false;
	RuntimeObject* V_3 = NULL;
	RuntimeObject* V_4 = NULL;
	{
		intptr_t L_0 = ___0_gcHandlePtr;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		V_2 = L_1;
		bool L_2 = V_2;
		if (!L_2)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_3), sizeof(RuntimeObject*));
		RuntimeObject* L_3 = V_3;
		V_4 = L_3;
		goto IL_0036;
	}

IL_001d:
	{
		intptr_t L_4 = ___0_gcHandlePtr;
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC L_5;
		L_5 = Unmarshal_FromIntPtrUnsafe_mB304834C5EC431CC0FBE13C6BB97066F222BC55C_inline(L_4, NULL);
		V_0 = L_5;
		RuntimeObject* L_6;
		L_6 = GCHandle_get_Target_m481F9508DA5E384D33CD1F4450060DC56BBD4CD5_inline((&V_0), NULL);
		V_1 = ((RuntimeObject*)Castclass((RuntimeObject*)L_6, il2cpp_rgctx_data(method->rgctx_data, 0)));
		RuntimeObject* L_7 = V_1;
		V_4 = L_7;
		goto IL_0036;
	}

IL_0036:
	{
		RuntimeObject* L_8 = V_4;
		return L_8;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t MarshalledUnityObject_Marshal_TisRuntimeObject_m286B34400A212037E8EBD53DBFEAD7D23CDE8051_gshared_inline (RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	bool V_0 = false;
	intptr_t V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		RuntimeObject* L_0 = ___0_obj;
		V_0 = (bool)((((RuntimeObject*)(RuntimeObject*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0016;
		}
	}
	{
		V_1 = 0;
		goto IL_001f;
	}

IL_0016:
	{
		RuntimeObject* L_2 = ___0_obj;
		intptr_t L_3;
		L_3 = MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_inline(L_2, il2cpp_rgctx_method(method->rgctx_data, 1));
		V_1 = L_3;
		goto IL_001f;
	}

IL_001f:
	{
		intptr_t L_4 = V_1;
		return L_4;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_gshared_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method) 
{
	{
		bool L_0 = __this->___hasValue;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_gshared_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method) 
{
	{
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_0 = __this->___value;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_get_Length_m36BD32453530B535FE60A8123643219FEAABC351_gshared_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____length;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____stringLength;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC Unmarshal_FromIntPtrUnsafe_mB304834C5EC431CC0FBE13C6BB97066F222BC55C_inline (intptr_t ___0_gcHandle, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnsafeUtility_As_TisIntPtr_t_TisGCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC_mD94F597CB91662C823496DA9FA9A7CF908734A78_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* L_0;
		L_0 = UnsafeUtility_As_TisIntPtr_t_TisGCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC_mD94F597CB91662C823496DA9FA9A7CF908734A78_inline((&___0_gcHandle), UnsafeUtility_As_TisIntPtr_t_TisGCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC_mD94F597CB91662C823496DA9FA9A7CF908734A78_RuntimeMethod_var);
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC L_1 = (*(GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC*)L_0);
		V_0 = L_1;
		goto IL_0010;
	}

IL_0010:
	{
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC L_2 = V_0;
		return L_2;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* GCHandle_get_Target_m481F9508DA5E384D33CD1F4450060DC56BBD4CD5_inline (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) 
{
	{
		bool L_0;
		L_0 = GCHandle_get_IsAllocated_m241908103D8D867E11CCAB73C918729825E86843_inline(__this, NULL);
		if (L_0)
		{
			goto IL_0013;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_1 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral4EBC86E0EACFCA522AEB82874860D0E248D782A5)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&GCHandle_get_Target_m481F9508DA5E384D33CD1F4450060DC56BBD4CD5_RuntimeMethod_var)));
	}

IL_0013:
	{
		intptr_t L_2 = __this->___handle;
		bool L_3;
		L_3 = GCHandle_CanDereferenceHandle_mAAAC42D1268CEF3FDD040A3D1574773D08140579_inline(L_2, NULL);
		if (!L_3)
		{
			goto IL_002c;
		}
	}
	{
		intptr_t L_4 = __this->___handle;
		RuntimeObject* L_5;
		L_5 = GCHandle_GetRef_mAC7E58E62417209DC41C99F66BA70F0C3AA18DA8_inline(L_4, NULL);
		return L_5;
	}

IL_002c:
	{
		intptr_t L_6 = __this->___handle;
		RuntimeObject* L_7;
		L_7 = GCHandle_GetTarget_mE0AF851834410E2AEA6285B2497751570236C794(L_6, NULL);
		return L_7;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_gshared_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, Il2CppChar* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method) 
{
	{
		Il2CppChar* L_0 = ___0_ptr;
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_1;
		memset((&L_1), 0, sizeof(L_1));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_1), L_0);
		__this->____pointer = L_1;
		int32_t L_2 = ___1_length;
		__this->____length = L_2;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool GCHandle_CanDereferenceHandle_mAAAC42D1268CEF3FDD040A3D1574773D08140579_inline (intptr_t ___0_handle, const RuntimeMethod* method) 
{
	{
		intptr_t L_0 = ___0_handle;
		return (bool)((((intptr_t)((intptr_t)(L_0&((intptr_t)1)))) == ((intptr_t)((intptr_t)0)))? 1 : 0);
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* GCHandle_GetRef_mAC7E58E62417209DC41C99F66BA70F0C3AA18DA8_inline (intptr_t ___0_handle, const RuntimeMethod* method) 
{
	{
		intptr_t L_0 = ___0_handle;
		void* L_1;
		L_1 = IntPtr_op_Explicit_m2728CBA081E79B97DDCF1D4FAD77B309CA1E94BF(L_0, NULL);
		RuntimeObject** L_2;
		L_2 = il2cpp_unsafe_as_ref<RuntimeObject*>((intptr_t*)L_1);
		RuntimeObject* L_3 = *((RuntimeObject**)L_2);
		return L_3;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* UnsafeUtility_As_TisIntPtr_t_TisGCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC_mD94F597CB91662C823496DA9FA9A7CF908734A78_gshared_inline (intptr_t* ___0_from, const RuntimeMethod* method) 
{
	{
		intptr_t* L_0 = ___0_from;
		return (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC*)(L_0);
	}
}
