using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Version
{
    private string versionNumber = "2.0.1.3";

    public string VersionNumber { get => versionNumber; }

    /* Universal 2.0.1.1
     * Before 27th
     * 
     * 基础核心：
     * 模型控制器 UniversalModelControl
     * 视图控制器 UniversalViewControl
     * 动画控制器 UniversalAnimatorControl
     * 声音控制器 UniversalAudioSourceControl
     * 摄像机控制器 UniversalCameraMachinePositionControl
     * 信息存储控制器 UniversalDontDestroyOnLoadControl
     * 
     * 控制器管理 ControllerManger
     * 
     * 视图父级组件类：OnChoiceView（选择题组件） OnClickEventView（点击组件） OnToggleStyleView（Toggle点击风格组件）
     * OnDragView（拖拽UI组件） OnInformationView（UI跟随组件） OnSliderView（Slider限制组件） OnTogglesView（Toogle判断组件）
     * 
     * 功能类(Function and Tools)：Timer、CameraMove、CameraControl...
     * 
     * UIUniversal通用文件夹：通用主页、欢迎页、错误页面
     * 
     * 基础插件：
     * DOTween
     * Odin Inspector
     */

    /* Universal 2.0.1.2
     * 2022-07-27
     * 更新 模型控制器 GetModels(string modelName)、SetModelActive(string modelName, bool isActive)
     */

    /* Universal 2.0.1.2
     * 2022-08-31
     * 优化 SpriteManage脚本 DOTween重置问题
     * 更新 信息存储控制器 UniversalDontDestroyOnLoadControl 为 UniversalOverall
     * 更新 摄像机控制器 UniversalCameraMachinePositionControl 为 UniversalCameraControl
     * 更新 DOTweenToTest(float delayedTimer, int loopTimes,DelayedCallback delayedCallback) 延时回调
     */

    /* Universal 2.0.1.2
     * 2022-10-09
     * 添加 模型控制器 UniversalModelControl :  public void SetModeTransforms(string modelName, int indexArray, int indexPosition, int indexRotation = -1) 
     * 优化 模型控制器 UniversalModelControl :  public Vector3[] GetModeTransforms(int indexArray) 更换索引方式防止之前模型名称和位置不一致
     */

    /* Universal 2.0.1.2
     * 2022-11-01
     * 优化 脚本 UniversalTools :  DOTweenToTest 为 DOTweenToDelay
     * 优化 UniversalCameraControl 功能函数
     */

    /* Universal 2.0.1.3
     * 2022-11-11
     * 优化 脚本 UniversalViewControl :  变量名字、增加ADD快速添加提示框组件功能
     * 添加 通用选择题带图2.0 模板
     * 添加 通用课后练习题3.0模板
     */
}
