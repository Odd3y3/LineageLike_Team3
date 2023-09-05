using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    Player _player;
    /// <summary>
    /// 인게임에서의 플레이어
    /// </summary>
    public Player myPlayer
    {
        get
        {
            if (_player == null)
                _player = FindObjectOfType<Player>();
            return _player;
        }
        set { _player = value; }
    }

    PlayerInfo _playerInfo;
    

    //세이브 로드 기능
    public void Save()
    {
        //scriptable object로 저장
    }

    public void Load()
    {
        //scriptable object에서 _playerInfo로 정보 가져오기
    }
}

public struct PlayerInfo
{
    //맵 (어느 맵에 있었는지)

    //인벤토리 아이템 정보

    //장비창 아이템 정보

    //골드 정보
    public int Gold;

    //현재 Lv
    //현재 경험치

    //플레이 시간?
}