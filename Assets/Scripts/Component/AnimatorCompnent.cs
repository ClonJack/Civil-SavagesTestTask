using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimator
{
    void Animator(Vector2 dirStep);
}
public class AnimatorCompnent : IAnimator
{
    private Animator animator;

    private IGameInput gameInput;
    private IRayGame rayGame;

    private float deltaTimeAnim;
    private float inverseDeltaSpeed;

    private Vector2 currentVelocity;

    private string currentAnim = string.Empty;
    public AnimatorCompnent(Animator animator, float deltaTimeAnim, float inverseDeltaSpeed, IGameInput gameInput, IRayGame rayGame)
    {
        this.animator = animator;
        this.rayGame = rayGame;
        this.gameInput = gameInput;

        this.deltaTimeAnim = deltaTimeAnim;
        this.inverseDeltaSpeed = inverseDeltaSpeed;
    }
    public void Animator(Vector2 dirStep)
    {
        var animPlay = rayGame.GetPointTag();

        var isJumpBlock = animPlay == "JumpBlock";

        animator.SetBool("IsSit", gameInput.IsSit);
        animator.SetBool("IsJumpBlock", isJumpBlock && gameInput.IsJump);

        var smoothDamps = new Vector2(Mathf.SmoothDamp(animator.GetFloat("X"), dirStep.x, ref currentVelocity.x, deltaTimeAnim),
            Mathf.SmoothDamp(animator.GetFloat("Y"), dirStep.y, ref currentVelocity.y, deltaTimeAnim));

        if (dirStep == Vector2.zero)
        {
            smoothDamps = new Vector2(Mathf.SmoothDamp(animator.GetFloat("X"), dirStep.x, ref currentVelocity.x, inverseDeltaSpeed),
             Mathf.SmoothDamp(animator.GetFloat("Y"), dirStep.y, ref currentVelocity.y, inverseDeltaSpeed));
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Trick"))
            smoothDamps = Vector2.zero;
        
        animator.SetFloat("X", smoothDamps.x);
        animator.SetFloat("Y", smoothDamps.y);
    }
}
