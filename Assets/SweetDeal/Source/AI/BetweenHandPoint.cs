using System;
using UnityEngine;

namespace SweetDeal.Source.AI
{
    public class BetweenHandPoint : MonoBehaviour
    {
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        [SerializeField] private Transform head;
        [SerializeField] private float heightFactor = 1.5f;

        private void Update()
        {
            transform.position = (leftHand.position + rightHand.position) / 2 - transform.up * heightFactor;
            transform.up = (head.position - transform.position);
        }
    }
}