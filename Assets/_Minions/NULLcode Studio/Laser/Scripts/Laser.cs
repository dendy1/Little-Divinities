/**************************************************************************************************/
/** 	© 2017 NULLcode Studio. License: https://creativecommons.org/publicdomain/zero/1.0/deed.ru
/** 	Разработано в рамках проекта: http://null-code.ru/
/**                       ******   Внимание! Проекту нужна Ваша помощь!   ******
/** 	WebMoney: R209469863836, Z126797238132, E274925448496, U157628274347
/** 	Яндекс.Деньги: 410011769316504
/**************************************************************************************************/

using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	[SerializeField] private float size = 100; // длинна луча
	[SerializeField] private Transform laser; // родительский объект модели луча
	[SerializeField] private LayerMask ignoreMask; // фильтр по слоям
	private float dist;

	public bool pause;	
	public Transform gunPoint; // точка откуда должен вылетать луч

	void Create() 
	{
		dist = size;
		Vector3 hit = gunPoint.position + (gunPoint.localPosition + gunPoint.forward * dist);
		Vector3 center = (gunPoint.position + hit)/2;

		RaycastHit line;
		if(Physics.Linecast(gunPoint.position, hit, out line, ~ignoreMask))
		{
			if(!line.collider.isTrigger)
			{
				dist = Vector3.Distance(gunPoint.position, line.point);
				center = (gunPoint.position + line.point)/2;
			}
		}

		laser.localScale = new Vector3(1, 1, dist/2);
		laser.position = center;
		laser.localPosition = new Vector3(0, 0, laser.localPosition.z);
		laser.gameObject.SetActive(true);
	}

	void LateUpdate()
	{
		if (!pause)
			Create();
	}
}
