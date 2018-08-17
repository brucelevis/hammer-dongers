using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	private bool hit;
	Rigidbody2D rb;
	SpriteEffector spriteEffector;
	public float walkSpeed = 5;
	public Animator animator;
	private bool dying = false;

	float dashDirX = 0;
	float dashDirY = -1;
	public float dashSpeed = 10;
	public float dashCooldown = 1.2f;
	bool dashEnabled = true;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		spriteEffector = GetComponent<SpriteEffector> ();
	}

	void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
	}

	public void Reset ()
	{
		var groundChecker = GetComponentInChildren<GroundChecker> ();
		groundChecker.Reset ();
		animator.Rebind ();
		rb.velocity = Vector2.zero;
	}

	public void StartStun ()
	{
		animator.SetTrigger ("Stun");
		//to disrespect
		dying = false;
	}

	public void Die ()
	{
		
		if (animator.GetBool ("Dashing") || dying)
			return;

		dying = true;
		rb.velocity = Vector2.zero;
		AudioManager.playSFX("fall", 0.75f, false);
		animator.SetTrigger ("Fall");
	}

	public void DestroySelf (){
		this.gameObject.SetActive(false);
	}

	public void Hit ()
	{
		if (!animator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) 
			animator.SetTrigger ("Attack");
	}

	public void StartDash(float dirX, float dirY){
		if (animator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") 
		|| animator.GetCurrentAnimatorStateInfo (0).IsTag ("Death") 
		|| animator.GetBool ("Dashing") 
		|| !dashEnabled
		|| dying) 
			return;

		// SoundManager.PlaySound (dashSFX);
		dashEnabled = false;
		AudioManager.playSFX("dash", 0.75f, false);
		dashDirX = dirX;
		dashDirY = dirY;
		animator.SetBool ("Dashing", true);
		Invoke ("StopDash", 0.2f);
	}

	public void StopDash (){
		animator.SetBool ("Dashing", false);
		Invoke ("EnableDash", dashCooldown);
	}

	public void EnableDash(){
		spriteEffector.FlashGreenOnce();
		dashEnabled = true;
	}

	void Flip (Vector2 velocity) 
	{
		transform.localScale = new Vector3 (velocity.x > 0 ? -1 : 1, transform.localScale.y, transform.localScale.z);
	}

	void SetMovementAnimation (Vector2 velocity)
	{	
		//animator.enabled = !(velocity.x == 0 && velocity.y == 0);
	
		var parameters = animator.parameters;
		var names = new List<string>();

		if (velocity.x != 0) {
			names.Add("Side");
		}

		if (velocity.y > 0)
			names.Add ("Up");
		
		if (velocity.y < 0)
			names.Add ("Down");

		foreach (var parameter in parameters)
			animator.SetBool (parameter.name, names.Contains (parameter.name));
	}

	void FlipToMatchVelocity (Vector2 velocity){
		if (velocity.x != 0) {
			Flip (velocity);
		}
	}

	public void Move (Vector2 velocity) {

		if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") 
		|| animator.GetCurrentAnimatorStateInfo(0).IsTag("Death")
		|| dying){
			rb.velocity = velocity = Vector2.zero;
			return;
		}

		if (animator.GetBool ("Dashing")) {
			Dash ();
		}else {
			Walk (velocity);
		}
	}

	void Dash() {
		Vector2 velocity = new Vector2 (dashDirX, dashDirY);
		rb.velocity = velocity * dashSpeed;
		if (rb.velocity.x != 0 && rb.velocity.y != 0)
			rb.velocity *= 0.65f;
		animator.SetBool ("Dashing", true);
		FlipToMatchVelocity (velocity);
	}

	void Walk(Vector2 velocity){
		rb.velocity = velocity * walkSpeed;
		if (rb.velocity.x != 0 && rb.velocity.y != 0)
			rb.velocity *= 0.75f;
		SetMovementAnimation (velocity);
		FlipToMatchVelocity (velocity);
	}
}
