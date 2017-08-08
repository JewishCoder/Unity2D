using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerController : MonoBehaviour
	{
		#region Data

		//Максимальная скорость.
		public float MaxSpeed = 10f;

		//Сила прыжка.
		public int JumpForce = 600;

		//Что является землей.
		public LayerMask WhatIsGround;

		//Дочерний элемент отслеживающий пересещение с землей.
		public Transform GroundCheck;

		//Радиус соприкосновения.
		private float _groundRadius = 0.2f;

		//В какую сторону смотрит персонаж.
		private bool _isFacingRight = true;

		//Аниматор персонажа.
		private Animator _anim;

		private Rigidbody2D _rb;
	
		//Стоит ли персонаж на земле.
		private bool _isGrounded = false;

		#endregion

		/// <summary> Разворачивает персонажа. </summary>
		private void Flip()
		{
			_isFacingRight = !_isFacingRight;

			var theScale = transform.localScale;
			theScale.x *= -1;

			transform.localScale = theScale;
		}

		/// <summary> Запускается перед началом игры. </summary>
		private void Start()
		{
			_anim = GetComponent<Animator>();
			_rb = GetComponent<Rigidbody2D>();
		}

		/// <summary> Срабатывает через определенный промежуток времени. </summary>
		private void FixedUpdate()
		{
			_isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _groundRadius, WhatIsGround);
			_anim.SetBool("Ground", _isGrounded);
			_anim.SetFloat("VSpeed", _rb.velocity.y);
		
			if(!_isGrounded) return;

			var move = Input.GetAxis("Horizontal");
			_anim.SetFloat("Speed", Mathf.Abs(move));

			_rb.velocity = new Vector2(move * MaxSpeed, _rb.velocity.y);

			if(move > 0 && !_isFacingRight) Flip();
			else if(move < 0 && _isFacingRight) Flip();
		}

		/// <summary> Срабатывает с каждым обновлением экрана. </summary>
		private void Update()
		{
			if(!_isGrounded || !Input.GetKeyDown(KeyCode.Space)) return;

			_anim.SetBool("Ground", false);
			_rb.AddForce(new Vector2(0, JumpForce));
		}

	}
}
