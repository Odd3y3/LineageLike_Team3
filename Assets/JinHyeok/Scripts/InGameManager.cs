using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    Player _player;
    /// <summary>
    /// �ΰ��ӿ����� �÷��̾�
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
    

    //���̺� �ε� ���
    public void Save()
    {
        //scriptable object�� ����
    }

    public void Load()
    {
        //scriptable object���� _playerInfo�� ���� ��������
    }
}

public struct PlayerInfo
{
    //�� (��� �ʿ� �־�����)

    //�κ��丮 ������ ����

    //���â ������ ����

    //��� ����
    public int Gold;

    //���� Lv
    //���� ����ġ

    //�÷��� �ð�?
}