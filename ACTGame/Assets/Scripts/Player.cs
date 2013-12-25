using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float z=-1f;


	CharacterController m_ch;
	Animation m_animation;
	Transform cam_transform;

	private float gravity=0.1f;
	private float m_moveSpeed=2f;
	private float rotationX;
	 
	Vector3 m_camRot;

	// Use this for initialization
	void Start () {
		cam_transform= Camera.main.transform;
		m_ch=GetComponent<CharacterController>();
		m_animation=this.animation;
		m_camRot = cam_transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.J)){
			m_animation.Play("Attack3-1");
		}
		else if(Input.GetKeyDown(KeyCode.K)){
			m_animation.Play("Attack3-2");
		}
		else if(Input.GetKeyDown(KeyCode.L)){
			m_animation.Play("Attack3-3");
		}
		else if(Input.GetKeyDown(KeyCode.U)){
			m_animation.Play("Attack4");
		}
		else if(Input.GetKeyDown(KeyCode.Space)){
			//this.gameObject.AddComponent<iTween>();

			m_animation.Play("Jump2");
			Hashtable args = new Hashtable();
			args.Add("easeType", iTween.EaseType.easeInOutExpo);
			//args.Add("speed",1f);
			args.Add("time",1f);
			//args.Add("looktarget",Vector3.zero);
			args.Add("loopType", "none");
			args.Add("x",this.transform.position.x);
			args.Add("y",this.transform.position.y+2);
			args.Add("z",this.transform.position.z);
			iTween.MoveTo(gameObject,args);

			//iTween.MoveTo(this.gameObject,new Vector3(this.transform.position.x,this.transform.position.y+2,this.transform.position.z),1);
		}
		else{
			 
		}

		Control();
	}





	void FixedUpdate(){
		m_ch.Move(new Vector3(0,-gravity,0));
	}


	//控制移动
	private void Control()
	{
		#region 主角
		float x = 0, y = 0, z = 0;
		y -= gravity * Time.deltaTime;
		z = Input.GetAxis("Vertical") * m_moveSpeed * Time.deltaTime;
		m_ch.Move(this.transform.TransformDirection(new Vector3(x, y, z)));

		if(z !=0){
			m_animation.Play("Run");
		}

		#endregion




		#region 摄像机跟随
		
		rotationX = Input.GetAxis("Horizontal");       
		 
		m_camRot.y += rotationX;
		cam_transform.eulerAngles = m_camRot;
		
		Vector3 camrot = cam_transform.eulerAngles;
		camrot.x = 0;
		camrot.z = 0;
		this.transform.eulerAngles = camrot;
		
//		Vector3 pos = this.transform.position;
//		pos.y += 0.1f;
//		pos.z=z;
//		cam_transform.position = pos;
		#endregion
	}
}
