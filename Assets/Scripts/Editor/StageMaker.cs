using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Assets.Scripts
{
    //3 streetEn speed 30
    class StageMaker
    {
        GameObject[] objects;
        void Layout()
        {
	    //var opt=UnityEngine.GUILayoutOption;
	    EditorGUIUtility.ObjectContent(null, typeof(UnityEngine.GameObject[]));
        }
        [MenuItem("Example/QuickHelper _h")]
        void push()
        {
        }

	    //�I�̓o��^�C�~���O���O����ύX�ł���悤��
	    //�X�e�[�W�̒�����c�����邽�߂ɐ�΃^�C�~���O�w��
//Game
// StageObserver
// Building Building
// Player player
// Enemy okonomienemy
// WallEnemy wallEnemy
//Building
// Speed 49
//Enemy
//  EnBullet SoiBullet
//  First ways
//  Second leftway2
//  AI FollowPath
//Player
//  PlayerBehav
//  Bullet bullet
//  TargetBox targetB
//TargetBox Rate 25
//targetB
    }
}
