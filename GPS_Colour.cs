using GTA;
using GTA.Native;
using GTA.Math;
using System;
using System.Windows.Forms;

public class GPS_Colour : Script {
    float Xb = 100f;
    float Yb = 100f;
    int a = -2;
    int b = -3;
    bool First = true;
    Ped _ped;
    Vector3 waypointPos;
    public GPS_Colour() {
        Tick += Set_GPS;
    }
    #region Fields
    private int _handle;
    #endregion
    public int Handle { get { return _handle; } }
    public ulong NativeValue {
        get { return (ulong)_handle; }
        set { _handle = unchecked((int)value); }
    }
    public Ped Character {
        get {
            int handle = Function.Call<int>(Hash.GET_PLAYER_PED, Handle);

            if (ReferenceEquals(_ped, null) || handle != _ped.Handle) {
                _ped = new Ped(handle);
            }

            return _ped;
        }
    }
    public int Get_Player() {

        int Characters;

        switch ((PedHash)Character.Model.Hash) {
            case PedHash.Michael:
            Characters = 1;
            break;
            case PedHash.Franklin:
            Characters = 2;
            break;
            case PedHash.Trevor:
            Characters = 3;
            break;
            default:
            Characters = 0;
            break;
        }
        return Characters;
    }
    private void Set_GPS(object sender, EventArgs e) {

        if (Game.IsWaypointActive) {
            waypointPos = World.GetWaypointPosition();
            Xb = waypointPos.X;
            Yb = waypointPos.Y;
            int Player = Get_Player();
            switch (Player) {
                case 1:
                Function.Call(Hash._0xF314CF4F0211894E, 142, 101, 180, 212, 255);
                a = 1;
                break;
                case 2:
                Function.Call(Hash._0xF314CF4F0211894E, 142, 171, 237, 171, 255);
                a = 2;
                break;
                case 3:
                Function.Call(Hash._0xF314CF4F0211894E, 142, 255, 163, 87, 255);
                a = 3;
                break;
                default:
                Function.Call(Hash._0xF314CF4F0211894E, 142, 164, 76, 242, 255);
                a = 0;
                break;
            }
            if (a != b) {
                Function.Call(Hash.SET_WAYPOINT_OFF);
                Function.Call(Hash.SET_NEW_WAYPOINT, Xb, Yb);
            }
            b = a;
        }
    }
}