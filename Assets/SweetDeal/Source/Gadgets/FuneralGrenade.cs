using System;
using SweetDeal.Source.Gadgets.Inventory.SpriteConfigurable;
using UnityEngine;

namespace SweetDeal.Source.Gadgets
{
    public class FuneralGrenade : Gadget
    {
        private GrenadeProjectile _grenadeProjectile;
        private Transform _spawnPoint;
        private float _throwHeight;
        private float _throwForce;
        private Transform _player;

        public FuneralGrenade(Transform spawnPoint, Transform player, int useNumbers, GadgetDefinition definition) :  base(useNumbers, definition)
        {
            _grenadeProjectile = definition.Projectile as GrenadeProjectile;
            _spawnPoint = spawnPoint;
            _throwHeight = definition.ThrowHeight;
            _throwForce = definition.ThrowForce;
            _player = player;
        }

        public override void Use()
        {
            UseNumbers -= 1;
            var grenade = GameObject.Instantiate(_grenadeProjectile, _spawnPoint.position, _spawnPoint.rotation);
            grenade.transform.parent = _spawnPoint;
            var direction = _player.forward;
            direction.y = _throwHeight;
            direction.Normalize();
            grenade.Launch(direction, _throwForce);
        }
    }
}