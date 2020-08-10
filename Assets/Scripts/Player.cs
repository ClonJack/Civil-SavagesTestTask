using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : TickComponent
{
    private IMove movePL;
    private ICamera cameraPL;
    private IAnimator animatorPL;
    private IWidget widgetPL;
    private IGameInput inputPL;
    private IRayGame rayGamePL;
    private IUtilitCharacterController utilitCharacterController;

    private CharacterController characterController;
    private Animator animator;
    private GameInput inputActions;
    private Camera cam;

    private Vector3 dir;
    private bool isCanGravity;

    [SerializeField] private Transform camera;
    [SerializeField] private Transform beginPoint;
    [SerializeField] private Transform target;

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image img;

    [Header("Animator")]
    [SerializeField] private float deltaSpeed;
    [SerializeField] private float inverseDeltaSpeed;
    private void ComponentsBind()
    {
        inputPL = new InputComponent(inputActions);
        rayGamePL = new RayGmComponent(beginPoint);
        utilitCharacterController = new UtilitCharacterControllerComponent();
        movePL = new MovementComponent(inputPL);
        cameraPL = new CameraComponent(camera, transform);
        animatorPL = new AnimatorCompnent(animator, deltaSpeed, inverseDeltaSpeed, inputPL, rayGamePL);
        widgetPL = new UIComponent();

    }
    public void EnterAnim() => isCanGravity = true;
    public void ExitAnim() => isCanGravity = false;
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private bool IsPlayTrick()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag("Trick");
    }
    protected override void InitializedObj()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        inputActions = new GameInput();
        ComponentsBind();

        cam = Camera.main;
    }
    protected override void Tick()
    {
        var dr = inputActions.Player.Movement.ReadValue<Vector2>();
        var isGrounded = utilitCharacterController.IsGrounded(transform, 0.2f);

        if (!isGrounded)
            dir = new Vector3(dr.x, 0, dr.y);
        else
            dir = Vector3.zero;

        if (IsPlayTrick())
            dir = Vector3.forward;

        dir = movePL.JumpPlayer(dir, isCanGravity, rayGamePL.GetPointTag(), isGrounded);

        animator.applyRootMotion = isGrounded || IsPlayTrick();

        characterController.Move(dir * Time.deltaTime);

        animatorPL.Animator(dr);
    }
    protected override void TickLate()
    {
        var dr = inputActions.Player.Mouse.ReadValue<Vector2>();
        if (!IsPlayTrick())
            cameraPL.Rotate(dr);
    }
    protected override void TickFixed()
    {
        img.transform.position = widgetPL.LookTarget(img, cam, target, transform);
    }
    public void DisplayHint(string text) => textMesh.text = "Нажмите:" + text;
    public void ClearHint() => textMesh.text = string.Empty;

}
