using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class UserSettings: MonoBehaviour
    {
        //�v���n�u�A�^�b�`�ł���悤�Ƀp�u���b�N�ϐ����`
        public GameObject textPrefab; // �v���n�u���A�^�b�`
        public GameObject textButtonPrefab; // �v���n�u���A�^�b�`
        public GameObject imagePrefab; // �v���n�u���A�^�b�`
        public Canvas canvas; // Hierarchy�r���[����Canvas���A�^�b�`
        //
        
        //���[�N�V���b�v��Î҂������������߂�.
        private readonly int userNumber;//���[�N�V���b�v�̃O���[�v������̐l�������߂�.
        /*�s������,�񂪋A���i�̍s����l����.
         *i�sj��̗v�f��a(ij)�Ƃ����,�Q���҂�����i�ɋA���ij��I�񂾏ꍇa(ij) = 1�����,����ȊO��0������.
         *���̍s���p���邱�Ƃ�,�ǂ̂悤�ȑI����I��ł����̂����킩��s��𓾂邱�Ƃ��ł���.
         *������A���i�̑I�𐄈ڍs��ƌĂԂ��Ƃɂ���.
         * ��1
         * {
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0},
        };
         * �����11������23���܂őS�ĉ�ЂɎc��Ƃ����I����I�񂾂��Ƃ�\��.
         * ���̍s���13�s����,�ŏ����琔����, 11��, 12��, 13��, 14��, 15��, 16��, 17��, 18��, 19��, 20��, 21��, 22��, 23��.
         * ���̍s���6�񂠂�,�ŏ����琔����, ��ЂɎc��, JR, ���S�{�^�N�V�[, �n���S+�^�N�V�[, �n���S+��Ƃ�[��+�k��, ���S+�k��.
         * ��2
         * {
         *  {1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
        };
        *  �����11���ɉ�ЂɎc�����Ƃ����I����I���,12���ȍ~�͂܂����m��ł��邱�Ƃ�\���s��ł���.
        *  
        *  �܂�,���̑I�𐄈ڍs����ȉ��̎菇�Ń��j�[�N(�����ɂȂ�Ȃ��悤)�Ȑ��l(int�l)�ɕϊ�����.
        *  i���Œ肵��j=1����6�܂ł̑��a��{a(ij)�~j}���ꎟ���z��b(i)�ɑ������.
        *  b(i)�̐��l�𕶎���ɕϊ�������,�ŏ����珇�ɕ�����Ƃ��ĘA������.�܂�i=1����13�܂ł̑��a��������{b(i)}.
        *  ����𐔒l�ɕϊ�.
        *  �Ⴆ�Η�1�Ȃ�,1111 1111 1111 1�ƂȂ�,��2�Ȃ�1000 0000 0000 0�ƂȂ�.�󔒂͂킩��₷�����邽�߂ɏ�����.
        * 
         */

        /*���L�ł�,�s�񂲂Ƃɂ܂�A���󋵂��ƂɎ��s���郁�\�b�h���`������̂ł���.
         * 
         * 11������i-1���܂ŉ�ЂɎc��I����I�񂾂Ƃ���,����i�̋A���i��\�����郁�\�b�h���`
         * 
         */
        public void Transition0()
        {
            GameObject textObject = Instantiate(textPrefab, canvas.transform);
            textObject.GetComponent<Text>().text = "Hello, World!";
        }
        /*
         * 
         * 11������i-1���̂����ꂩ�ŉ�ЂɎc��ȊO�̑I����I�񂾂Ƃ���,����i�̋A���󋵂�\�����郁�\�b�h���`
         * 
         */
        public UserSettings(int userNum)
        {
            userNumber = userNum;
        }

    }

}