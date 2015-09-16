using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		public float MovementRange = 30f;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input
        RectTransform rectTransform ;
		Vector2 m_StartPos;
		bool m_UseX; // Toggle for using the x axis
		bool m_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input
        private Vector2 origin;

        void OnEnable()
		{
			CreateVirtualAxes();
		}

        void Start()
        {
            //m_StartPos = transform.position;
            rectTransform = GetComponent<RectTransform>();
            m_StartPos = rectTransform.anchoredPosition;
            origin = m_StartPos;
        }

		void UpdateVirtualAxes(Vector2 value)
        {
            var delta = origin - value;
			delta.y = -delta.y;
			delta /= MovementRange;
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Update(-delta.x);
                //Debug.Log("Value: " + value + ", Start: " + origin);
            }

			if (m_UseY)
			{
				m_VerticalVirtualAxis.Update(delta.y);
			}
		}

		void CreateVirtualAxes()
		{
			// set axes to use
			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			// create new axes based on axes to use
			if (m_UseX)
			{
				m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
			}
		}


		public void OnDrag(PointerEventData data)
		{
			Vector3 newPos = Vector3.zero;

			if (m_UseX)
			{
				float delta = (float)(data.position.x - m_StartPos.x);
				delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
				newPos.x = delta;
			}

			if (m_UseY)
			{
                float delta = (float)(data.position.y - m_StartPos.y);
				delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta;
			}
            //transform.position = (Vector3.ClampMagnitude(new Vector3(newPos.x, newPos.y, 0), MovementRange)+m_StartPos);
            rectTransform.anchoredPosition = new Vector2(newPos.x , newPos.y);
            //Debug.Log("Position: "+ rectTransform.anchoredPosition);
			UpdateVirtualAxes(rectTransform.anchoredPosition);
		}


		public void OnPointerUp(PointerEventData data)
		{
            rectTransform.anchoredPosition = origin;
			UpdateVirtualAxes(origin);
		}


		public void OnPointerDown(PointerEventData data) {
            m_StartPos = data.position;
            //Debug.Log("Dn: " + data.position+" "+ rectTransform.anchoredPosition);
        }

		void OnDisable()
		{
			// remove the joysticks from the cross platform input
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Remove();
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis.Remove();
			}
		}
	}
}