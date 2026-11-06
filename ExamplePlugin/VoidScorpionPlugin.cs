using BepInEx;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VoidScorpion
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [BepInPlugin("com.Slendykray.VoidSymbioticScorpion", "VoidSymbioticScorpion", "1.0.0")]
    public class VoidScorpionPlugin : BaseUnityPlugin
    {
        public void Awake()
        {
            Log.Init(Logger);

            Asset.Init();

            ItemDef hammer = Addressables.LoadAssetAsync<ItemDef>("RoR2/Base/ArmorReductionOnHit/ArmorReductionOnHit.asset").WaitForCompletion();
            ItemDef scorpion = Addressables.LoadAssetAsync<ItemDef>("RoR2/DLC1/PermanentDebuffOnHit/PermanentDebuffOnHit.asset").WaitForCompletion();

            scorpion._itemTierDef = Addressables.LoadAssetAsync<ItemTierDef>("RoR2/DLC1/Common/VoidTier3Def.asset").WaitForCompletion();

            scorpion.pickupToken = "Permanently reduce armor on hit. <style=cIsVoid>Corrupts all Shattering Justices</style>.";
            scorpion.descriptionToken = "<style=cIsDamage>100%</style> chance on hit to reduce <style=cIsDamage>armor</style> by <style=cIsDamage>2</style> <style=cStack>(+2 per stack)</style> <style=cIsDamage>permanently</style>. <style=cIsVoid>Corrupts all Shattering Justices</style>.";
            scorpion.pickupIconSprite = Asset.mainBundle.LoadAsset<Sprite>("icon.png");

            var provider = ScriptableObject.CreateInstance<ItemRelationshipProvider>();
            provider.name = "ScorpionContagiousItemProvider";
            provider.relationshipType = Addressables.LoadAssetAsync<ItemRelationshipType>("RoR2/DLC1/Common/ContagiousItem.asset").WaitForCompletion();
            provider.relationships =
            [
                new ItemDef.Pair
                {
                    itemDef1 = hammer,
                    itemDef2 = scorpion
                }

            ];
            ContentAddition.AddItemRelationshipProvider(provider);
        }

    }
}
