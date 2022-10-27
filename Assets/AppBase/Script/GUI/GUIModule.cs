//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using Asset = UnityEngine.Object;
//using Scene = System.Enum;
//using UnityEngine.UI;
//using UnityEngine.Events;

//public static partial class GUIModule
//{
//    public static class ButtonUI
//    {
//        public static void ClearEvent(Button button)
//        {
//            button.onClick.RemoveAllListeners();
//            //UIEventTrigger eventTrigger = AssetObject.FindComponent<UIEventTrigger>(button.gameObject);
//            //if (eventTrigger != null)
//            //{
//            //    eventTrigger.onPress.Clear();
//            //    eventTrigger.onHoverOver.Clear();
//            //    eventTrigger.onDragOut.Clear();
//            //}
//        }

//        public static void RemoveClickEvent(Button button, UnityAction eventDelegate)
//        {
//            button.onClick.RemoveListener(eventDelegate);

//#if UNITY_WSA || UNITY_WSA_10_0
//            var input = AssetObject.FindComponent<EventHoloLens>(button.gameObject);
//            if (input == null)
//            {
//                AssetObject.RemoveComponent<EventHoloLens>(button.gameObject);
//            }
//#endif

//        }

//        public static void AddOnClickEvent(Button button, UnityAction eventDelegate)
//        {
//            button.onClick.Add(eventDelegate);

//#if UNITY_WSA || UNITY_WSA_10_0
//            var input = AssetObject.FindComponent<EventHoloLens>(button.gameObject);
//            if (input == null)
//            {
//                button.gameObject.AddComponent<EventHoloLens>();
//                var inputNew = AssetObject.FindComponent<EventHoloLens>(button.gameObject);
//                inputNew.SetCallback(button, eventDelegate);
//            }
//#endif
//        }
//        public static void AddOnPressEvent(Button button, UnityAction eventDelegate)
//        {
//            UIEventTrigger eventTrigger = AssetObject.FindComponent<UIEventTrigger>(button.gameObject);
//            //if (eventTrigger == null)
//            //{
//            //    eventTrigger = AssetObject.InsertComponent<UIEventTrigger>(button.gameObject);
//            //}
//            //eventTrigger.onPress.Add(eventDelegate);

//#if UNITY_WSA || UNITY_WSA_10_0
//            var input = AssetObject.FindComponent<EventHoloLens>(button.gameObject);
//            if (input == null)
//                button.gameObject.AddComponent<EventHoloLens>();

//            var inputNew = AssetObject.FindComponent<EventHoloLens>(button.gameObject);
//            inputNew.SetCallback(button, null, eventDelegate, null);
//#endif

//        }
//        public static void AddOnReleaseEvent(Button button, UnityAction eventDelegate)
//        {
//            //UIEventTrigger eventTrigger = AssetObject.FindComponent<UIEventTrigger>(button.gameObject);
//            //if (eventTrigger == null)
//            //{
//            //    eventTrigger = AssetObject.InsertComponent<UIEventTrigger>(button.gameObject);
//            //}
//            //eventTrigger.onRelease.Add(eventDelegate);

//#if UNITY_WSA || UNITY_WSA_10_0
//            var input = AssetObject.FindComponent<EventHoloLens>(button.gameObject);
//            if (input == null)
//                button.gameObject.AddComponent<EventHoloLens>();

//            var inputNew = AssetObject.FindComponent<EventHoloLens>(button.gameObject);
//            inputNew.SetCallback(button, null, null, eventDelegate);
//#endif
//        }

//        public static void AddOnDragOutEvent(Button button, UnityAction eventDelegate )
//        {
//            //UIEventTrigger eventTrigger = AssetObject.FindComponent<UIEventTrigger>( button.gameObject );
//            //if ( eventTrigger == null )
//            //{
//            //    eventTrigger = AssetObject.InsertComponent<UIEventTrigger>( button.gameObject );
//            //}
//            //eventTrigger.onDragOut.Add( eventDelegate );
//        }

//        public static void AddOnDragOverEvent(Button button, UnityAction eventDelegate)
//        {
//            //UIEventTrigger eventTrigger = AssetObject.FindComponent<UIEventTrigger>(button.gameObject);
//            //if(eventTrigger == null)
//            //{
//            //    eventTrigger = AssetObject.InsertComponent<UIEventTrigger>(button.gameObject);
//            //}
//            //eventTrigger.onDragOver.Add(eventDelegate);
//        }

//        public static void AddOnHoverOverEvent(UIButton button, EventDelegate eventDelegate)
//        {
//            UIEventTrigger eventTrigger = AssetObject.FindComponent<UIEventTrigger>(button.gameObject);
//            if (eventTrigger == null)
//            {
//                eventTrigger = AssetObject.InsertComponent<UIEventTrigger>(button.gameObject);
//            }
//            eventTrigger.onHoverOver.Add(eventDelegate);
//        }

//        public static void AddOnHoverOutEvent(UIButton button, EventDelegate eventDelegate)
//        {
//            UIEventTrigger eventTrigger = AssetObject.FindComponent<UIEventTrigger>(button.gameObject);
//            if (eventTrigger == null)
//            {
//                eventTrigger = AssetObject.InsertComponent<UIEventTrigger>(button.gameObject);
//            }
//            eventTrigger.onHoverOut.Add(eventDelegate);
//        }
//    }
//    public static class Event
//    {
//        public static EventDelegate Create(EventDelegate.Callback func)
//        {
//            EventDelegate eventDelegate = new EventDelegate(func);
//            return eventDelegate;
//        }
//        public static EventDelegate Create(MonoBehaviour target, string methodName, params object[] array)
//        {
//            EventDelegate eventDelegate = new EventDelegate(target, methodName);
//            if (array != null)
//            {
//                for (int i = 0; i < array.Length; i++)
//                {
//                    eventDelegate.parameters[i] = new EventDelegate.Parameter(array[i]);
//                }
//            }
//            return eventDelegate;
//        }

//        public static void CreateBackground(UISprite s_Background)
//        {
//            if (s_Background != null)
//            {
//                GameObject target = s_Background.gameObject;
//                BoxCollider collider = AssetObject.FindComponent<BoxCollider>(target);
//                if (collider == null)
//                {
//                    collider = AssetObject.InsertComponent<BoxCollider>(target);
//                }
//                collider.size = new Vector3(s_Background.width, s_Background.height, 0.0f);
//            }
//        }
//    }
//    public static class Input
//    {
//        public static void AddOnSubmitEvent(UIInput input, EventDelegate.Callback func)
//        {
//            if (input != null)
//            {
//                input.onSubmit.Add(Event.Create(func));
//            }
//        }

//        public static void AddOnChangeEvent(UIInput input, EventDelegate.Callback func)
//        {
//            if (input != null)
//            {
//                input.onChange.Add(Event.Create(func));
//            }
//        }
//    }
//    public static class Label
//    {
//        public static void Modify(Text label, string text)
//        {
//            label.text = text;
//            if(label.printedSize.y / label.fontSize > 1)
//            {
//                label.spacingY = NewConfigData.LABEL_SPACING_Y;
//            }
//        }
//        public static void Clear(Text label)
//        {
//            label.text = "";
//        }
//        public static string GetValue(Text label)
//        {
//            return label.text;
//        }
//    }
//    public static class Direction
//    {
//        public static void FadeClear(GameObject Target)
//        { 
//            TweenAlpha taAnimation = AssetObject.FindComponent<TweenAlpha>(Target);
//            if (taAnimation != null)
//            {
//                taAnimation.onFinished.Clear();
//            }
//        }
//        public static void FadeIn(GameObject Target, float duratinon, EventDelegate edEvent)
//        {
//            FadeIn(Target, duratinon, 0.0f, edEvent);
//        }
//        public static void FadeIn_Half(GameObject Target, float duratinon, EventDelegate edEvent)
//        {
//            FadeIn_Half(Target, duratinon, 0.0f, edEvent);
//        }
//        public static void FadeIn(GameObject Target, float duratinon, float delay, EventDelegate edEvent)
//        {
//            TweenAlpha taAnimation = AssetObject.FindComponent<TweenAlpha>(Target);
//            if (taAnimation == null)
//            {
//                taAnimation = AssetObject.InsertComponent<TweenAlpha>(Target);
//            }
//            taAnimation.from = 0;
//            taAnimation.to = 1;
//            taAnimation.style = UITweener.Style.Once;
//            taAnimation.tweenGroup = 0;
//            taAnimation.duration = duratinon;
//            taAnimation.delay = delay;
//            taAnimation.ignoreTimeScale = true;

//            taAnimation.ResetToBeginning();
//            taAnimation.onFinished.Clear();

//            if (edEvent != null)
//            {
//                taAnimation.AddOnFinished(edEvent);
//            }
//            taAnimation.PlayForward();
//        }
//        public static void FadeIn_Half(GameObject Target, float duratinon, float delay, EventDelegate edEvent)
//        {
//            TweenAlpha taAnimation = AssetObject.FindComponent<TweenAlpha>(Target);
//            if (taAnimation == null)
//            {
//                taAnimation = AssetObject.InsertComponent<TweenAlpha>(Target);
//            }
//            taAnimation.from = 0;
//            taAnimation.to = 0.5f;
//            taAnimation.style = UITweener.Style.Once;
//            taAnimation.tweenGroup = 0;
//            taAnimation.duration = duratinon;
//            taAnimation.delay = delay;
//            taAnimation.ignoreTimeScale = true;

//            taAnimation.ResetToBeginning();
//            taAnimation.onFinished.Clear();

//            if (edEvent != null)
//            {
//                taAnimation.AddOnFinished(edEvent);
//            }
//            taAnimation.PlayForward();
//        }
//        public static void FadeOut(GameObject Target, float duratinon, EventDelegate edEvent)
//        {
//            FadeOut(Target, duratinon, 0.0f, edEvent);
//        }
//        public static void FadeOut(GameObject Target, float duratinon, float delay, EventDelegate edEvent)
//        {
//            TweenAlpha ta = AssetObject.FindComponent<TweenAlpha>(Target);
//            if (ta == null)
//            {
//                ta = AssetObject.InsertComponent<TweenAlpha>(Target);
//            }
//            ta.from = 1;
//            ta.to = 0;
//            ta.style = UITweener.Style.Once;
//            ta.duration = duratinon;
//            ta.delay = delay;
//            ta.ignoreTimeScale = true;

//            ta.ResetToBeginning();
//            ta.onFinished.Clear();

//            if (edEvent != null)
//            {
//                ta.AddOnFinished(edEvent);
//            }
//            ta.PlayForward();
//        }
        
//        public static void Slide(GameObject Target, Vector3 from, Vector3 to, float time)
//        {
//            Slide(Target, from, to, time, 0.0f, UITweener.Style.Once, UITweener.Method.EaseIn, null);
//        }
//        public static void Slide(GameObject Target, Vector3 from, Vector3 to, float time, EventDelegate edEvent)
//        {
//            Slide(Target, from, to, time, 0.0f, UITweener.Style.Once, UITweener.Method.EaseIn, edEvent);
//        }
//        public static void Slide(GameObject Target, Vector3 from, Vector3 to, float time, float delay, EventDelegate edEvent)
//        {
//            Slide(Target, from, to, time, delay, UITweener.Style.Once, UITweener.Method.EaseIn, null);
//        }
//        public static void Slide(GameObject Target, Vector3 from, Vector3 to, float time, UITweener.Style style)
//        {
//            Slide(Target, from, to, time, 0.0f, style, UITweener.Method.EaseIn, null);
//        }
//        public static void Slide(GameObject Target, Vector3 from, Vector3 to, float time, UITweener.Style style, UITweener.Method method, EventDelegate edEvent, bool WorldSpace = false)
//        {
//            Slide(Target, from, to, time, 0.0f, UITweener.Style.Once, method, edEvent, WorldSpace);
//        }
//        public static void Slide(GameObject Target, Vector3 from, Vector3 to, float time, float delay, UITweener.Style style, UITweener.Method method, EventDelegate edEvent, bool WorldSpace = false)
//        {
//            TweenPosition taAnimation = AssetObject.FindComponent<TweenPosition>(Target, Target.name);
//            if (taAnimation == null)
//            {
//                taAnimation = AssetObject.InsertComponent<TweenPosition>(Target);
//            }

//            taAnimation.ResetToBeginning();
//            taAnimation.from = from;
//            taAnimation.to = to;
//            taAnimation.style = style;
//            taAnimation.duration = time;
//            taAnimation.delay = delay;
//            taAnimation.ignoreTimeScale = true;
//            taAnimation.method = method;
//            taAnimation.worldSpace = WorldSpace;
//            taAnimation.onFinished.Clear();

//            if (edEvent != null)
//            {
//                taAnimation.AddOnFinished(edEvent);
//            }
//            taAnimation.PlayForward();
//        }
        
//        public static void RotationZ(GameObject Target, float fRotationDgree,UITweener.Style style)
//        {
//            TweenRotation taAnimation = AssetObject.FindComponent<TweenRotation>(Target);
//            if (taAnimation == null)
//            {
//                taAnimation = AssetObject.InsertComponent<TweenRotation>(Target);
//            }

//            Target.transform.localPosition = Vector3.zero;
//            taAnimation.ResetToBeginning();
//            taAnimation.from = Vector3.zero;
//            taAnimation.to = Vector3.forward * fRotationDgree;
//            taAnimation.style = style;
//            taAnimation.duration = 1.3f;
//            taAnimation.delay = 0.0f;
//            taAnimation.ignoreTimeScale = true;

//            taAnimation.PlayForward();
//        }
//        public static void Rotation(GameObject Target, Vector3 vRotation, UITweener.Style style)
//        {
//            TweenRotation taAnimation = AssetObject.FindComponent<TweenRotation>(Target);
//            if (taAnimation == null)
//            {
//                taAnimation = AssetObject.InsertComponent<TweenRotation>(Target);
//            }

//            Target.transform.localPosition = Vector3.zero;
//            taAnimation.ResetToBeginning();
//            taAnimation.from = Vector3.zero;
//            taAnimation.to = vRotation;
//            taAnimation.style = style;
//            taAnimation.duration = 1.0f;
//            taAnimation.delay = 0.0f;
//            taAnimation.ignoreTimeScale = true;

//            taAnimation.PlayForward();
//        }

//        public static void TutorialHand(GameObject hand_obj)
//        {
//            if (hand_obj != null)
//            {
//                TweenPosition tp = AssetObject.InsertComponent<TweenPosition>(hand_obj);
//                tp.from = new Vector3(0.0f, 0.0f, 0.0f);
//                tp.to = new Vector3(0.0f, 0.0f, 0.0f);
//                tp.style = UITweener.Style.PingPong;
//                tp.duration = 0.5f;

//                TweenScale ts = AssetObject.InsertComponent<TweenScale>(hand_obj);
//                ts.from = Vector3.one * 0.9f;
//                ts.to = Vector3.one * 1.0f;
//                ts.style = UITweener.Style.PingPong;
//                ts.duration = 0.5f;
//            }
//        }
//        public static void Arrow(GameObject obj)
//        {
//            if (obj != null)
//            {
//                TweenPosition tp = AssetObject.InsertComponent<TweenPosition>(obj);
//                tp.from = new Vector3(60.0f, -60.0f, 0.0f);
//                tp.to = new Vector3(85.0f, -85.0f, 0.0f);
//                tp.style = UITweener.Style.PingPong;
//                tp.duration = 0.5f;

//                TweenScale ts = AssetObject.InsertComponent<TweenScale>(obj);
//                ts.from = Vector3.one * 0.9f;
//                ts.to = Vector3.one * 1.1f;
//                ts.style = UITweener.Style.PingPong;
//                ts.duration = 0.5f;
//            }
//        }
//    }
//    public static class Layer
//    {
//        public static int DefaultUI
//        {
//            get
//            {
//                return LayerMask.NameToLayer("UI");
//            }
//        }
//        public static int TutorialUI
//        {
//            get
//            {
//                return 8;
//            }
//        }
//        public static int DirectionUI
//        {
//            get
//            {
//                return 8;
//            }
//        }
//        public static int ConverterLayerMask(int layer)
//        {
//            return 1 << layer;
//        }
//        public static int ConverterLayerValue(int layer)
//        {
//            return 1 >> layer;
//        }

//        public static void Modify(GameObject ui, int Layer)
//        {
//            ui.layer = Layer;

//            for (int i = 0; i < ui.transform.childCount; i++)
//            {
//                Transform child = ui.transform.GetChild(i);
//                if (null == child)
//                {
//                    continue;
//                }
//                Modify(child.gameObject, Layer);
//            }
//        }
//        public static void DefaultModify(GameObject ui)
//        {
//            GUIModule.Layer.Modify(ui, DefaultUI);
//        }
//        public static void TutorialModify(GameObject ui)
//        {
//            GUIModule.Layer.Modify(ui, TutorialUI);
//        }
//        public static void DirectionUIModify(GameObject ui)
//        {
//            GUIModule.Layer.Modify(ui, DirectionUI);
//        }
//    }
//}
