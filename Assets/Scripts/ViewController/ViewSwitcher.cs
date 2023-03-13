using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ViewSwitcher : MonoBehaviour
{
    public ViewType desiredViewType;

    ViewManager viewManager;

    private void Start()
    {
        viewManager = ViewManager.GetInstance();
    }

    public void SwitchViewWithCode(ViewType desiredViewType)
    {
        viewManager.SwitchView(desiredViewType);
    }

    public void SwitchViewWithInspector()
    {
        viewManager.SwitchView(desiredViewType);
    }
}