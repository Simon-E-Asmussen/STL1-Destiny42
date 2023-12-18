using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using UnityEngine;

public class BossWeapon : NetworkBehaviour
{

    [SerializeField] private List<ABossWeapon> _weapons = new List<ABossWeapon>();

    [SerializeField] private ABossWeapon currentWeapon;

    //private readonly SyncVar<int> _currentWeaponIndex = new(-1);
    
    [SyncVar(Channel = Channel.Unreliable, OnChange = nameof(OnCurrentWeaponIndexChanged))]
    private int _currentWeaponIndex = -1;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!base.IsOwner)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentWeapon.AOEMarker.SetActive(true);
            currentWeapon.AOEMarker.GetComponent<AOETrigger>().markerVisible = true;
            Debug.Log("Marker active");
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            currentWeapon.AOEMarker.GetComponent<AOETrigger>().markerVisible = false;
            FireWeapon();
            //currentWeapon.AOEMarker.SetActive(false);
            StartCoroutine(TurnMarkerOff());
            Debug.Log("Marker disabled");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InitializeWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InitializeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InitializeWeapon(2);
        }
    }

    public void InitializeWeapons(Transform parentOfWeapons)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].transform.SetParent(parentOfWeapons);
        }

        InitializeWeapon(0);
    }

    private void InitializeWeapon(int weaponIndex)
    {
        SetWeaponIndex(weaponIndex);
    }

    [ServerRpc]
    private void SetWeaponIndex(int weaponIndex)
    {
        _currentWeaponIndex = weaponIndex;
    }

    private void OnCurrentWeaponIndexChanged(int oldIndex, int newIndex, bool asServer)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].gameObject.SetActive(false);
        }
        
        if (_weapons.Count > newIndex)
        {
            currentWeapon = _weapons[newIndex];
            currentWeapon.gameObject.SetActive(true);
        }
    }

    private void FireWeapon()
    {
        currentWeapon.Fire();
    }

    IEnumerator TurnMarkerOff()
    {
        yield return new WaitForSeconds(.1f);
        currentWeapon.AOEMarker.SetActive(false);
    }
}