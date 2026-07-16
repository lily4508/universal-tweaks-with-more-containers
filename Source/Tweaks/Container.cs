using Il2CppTLD.IntBackedUnit;

using UniversalTweaks.Properties;

namespace UniversalTweaks.Tweaks;

// This entire class needs to be revisited with the following changes...
// Make sure that all of these containers are up-to-date, it has been awhile since it was last touched.
internal static class Container
{
    [HarmonyPatch(typeof(Il2Cpp.Container), nameof(Il2Cpp.Container.BeginContainerOpen))]
    private class AdjustCapacityOnOpen
    {
        public static void Postfix(Il2Cpp.Container __instance)
        {
            float? capacity = GetCapacity(__instance);
            if (capacity.HasValue)
            {
                __instance.m_Capacity = ItemWeight.FromKilograms(capacity.Value);
            }

            MelonLogger.Msg($"[UT] {GetPath(__instance.transform)} | cap={__instance.m_Capacity.ToFormattedStringWithUnits()}");
        }
    }

    [HarmonyPatch(typeof(Il2Cpp.Container), nameof(Il2Cpp.Container.Awake))]
    private class AdjustCapacityOnAwake
    {
        private static void Postfix(Il2Cpp.Container __instance)
        {
            float? capacity = GetCapacity(__instance);
            if (capacity.HasValue)
            {
                __instance.m_Capacity = ItemWeight.FromKilograms(capacity.Value);
            }
        }
    }

    /// <summary>Walks up the parent chain looking for a name containing the token.</summary>
    private static bool HasAncestor(Transform t, string token, int maxDepth = 4)
    {
        Transform p = t.parent;
        for (int i = 0; p != null && i < maxDepth; i++, p = p.parent)
        {
            if (p.name.Contains(token))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>Full hierarchy path, for logging.</summary>
    private static string GetPath(Transform t)
    {
        string path = t.name;
        for (Transform p = t.parent; p != null; p = p.parent)
        {
            path = p.name + "/" + path;
        }
        return path;
    }


    /// <summary>Get the capacity in kilograms to set for a container.</summary>
    /// <param name="container">The container component.</param>

    public static float? GetCapacity(Il2Cpp.Container container)
    {
        string name = container.name;
        Transform t = container.transform;

        if (Settings.Instance.InfiniteContainerWeight)
        {
            return 10000;
        }

        // Bathroom large Cabinet with shelving 
        if (name.Contains("CONTAINER_BathroomCabinet"))
        {
            return Settings.Instance.ContainerCabinetLgeCapacity;
        }

        // Large Cabinet
        if (name.Contains("CONTAINER_LargeCabinet"))
        {
            return Settings.Instance.ContainerCabinetLgeCapacity;
        }

        // Small Cabinet 
        if (name.Contains("CONTAINER_SmallCabinet"))
        {
            return Settings.Instance.ContainerCabinetSmlCapacity;
        }

        // Small Cabinet 2
        if (name.Contains("OBJ_SmallCabinetDoor"))
        {
            return Settings.Instance.ContainerCabinetSmlCapacity;
        }

        // Dresser Drawer
        if (name.Contains("OBJ_DresserDrawer") || name.Contains("OBJ_DresserTallDrawer"))
        {
            return Settings.Instance.ContainerDresserDrawerCapacity;
        }

        // Side Table Drawer
        if (name.Contains("OBJ_EndTableDrawer"))
        {
            return Settings.Instance.ContainerEndTableDrawerCapacity;
        }

        // Cupboard
        if (name.Contains("OBJ_CupboardDoor"))
        {
            return Settings.Instance.ContainerCupboardCapacity;
        }

        // Kitchen Cabinet
        if (name.Contains("OBJ_KitchenCabinetDoor"))
        {
            return Settings.Instance.ContainerKitchenCabinetCapacity;
        }

        // Kitchen Drawer
        if (name.Contains("OBJ_KitchenDrawer"))
        {
            return Settings.Instance.ContainerKitchenDrawerCapacity;
        }

        // Blue Trunk (Box) 1
        if (name.Contains("CONTAINER_LilysChest"))
        {
            return Settings.Instance.ContainerTrunkCapacity;
        }

        // Trunk (Box)
        if (name.Contains("CONTAINER_SteamerTrunk"))
        {
            return Settings.Instance.ContainerTrunkCapacity;
        }

        // Trunk (Rustic Storage Trunk Box)
        if (name.Contains("CONTAINER_Rustic_Storage_Trunk"))
        {
            return Settings.Instance.ContainerRusticStorageTrunkCapacity;
        }
       
        // Metal Desk Drawer Large
        if (name.Contains("OBJ_MetalDeskDrawer1") || name.Contains("OBJ_MetalDeskDrawer4"))
        {
            return Settings.Instance.ContainerDeskDrawerLgeCapacity;
        }

        // Metal Desk Drawer Small
        if (name.Contains("OBJ_MetalDeskDrawer2") || name.Contains("OBJ_MetalDeskDrawer3"))
        {
            return Settings.Instance.ContainerDeskDrawerSmlCapacity;
        }

        // Wood Desk Drawer
        if (name.Contains("OBJ_TrailerInteriorDeskDrawerLg_Prefab"))
        {
            return Settings.Instance.ContainerWoodDeskDrawerCapacity;
        }

        // Warden Desk
        if (name.Contains("OBJ_WardenDesk"))
        {
            return Settings.Instance.ContainerWardenDeskDrawerCapacity;
        }

        // File Cabinet
        if (name.Contains("OBJ_MetalFileCabinetDrawer"))
        {
            return Settings.Instance.ContainerFileCabinetCapacity;
        }

        // Tool Cabinet Drawers
        if (name.Contains("OBJ_ToolCabinetDrawer"))
        {
            return name.Contains("OBJ_ToolCabinetDrawerE")
                ? Settings.Instance.ContainerToolCabinetDrawerLgeCapacity
                : Settings.Instance.ContainerToolCabinetDrawerSmlCapacity;
        }

        // Workbench Drawer
        if (name.Contains("OBJ_WorkBenchDrawer"))
        {
            return Settings.Instance.ContainerWorkbenchDrawerCapacity;
        }

        // Fishing Hut Cupboard
        if (name.Contains("OBJ_FishingCabinCupboardDoor"))
        {
            return Settings.Instance.ContainerCupboardCapacity;
        }

        // Fishing Hut Drawers
        if (name.Contains("OBJ_FishingCabinDresserDrawer"))
        {
            return Settings.Instance.ContainerFishingHutDrawerCapacity;
        }

        // Oven
        if (name.Contains("OBJ_GasOvenDoor"))
        {
            return Settings.Instance.ContainerOvenCapacity;
        }

        // Fridge
        if (name.Contains("OBJ_FridgeBottomDoor"))
        {
            return Settings.Instance.ContainerFridgeCapacity;
        }

        // Freezer
        if (name.Contains("OBJ_FridgeTopDoor"))
        {
            return Settings.Instance.ContainerFreezerCapacity;
        }

        // Cooler
        if (name.Contains("CONTAINER_Cooler"))
        {
            return Settings.Instance.ContainerCoolerCapacity;
        }

        // Trash Can
        if (name.Contains("CONTAINER_TrashCanister"))
        {
            return Settings.Instance.ContainerTrashCanCapacity;
        }

        // Washer
        if (name.Contains("CONTAINER_Washer"))
        {
            return Settings.Instance.ContainerWasherCapacity;
        }

        // Dryer
        if (name.Contains("CONTAINER_Dryer"))
        {
            return Settings.Instance.ContainerDryerCapacity;
        }

        //Medicine Shelf
        if (name.Contains("CONTAINER_MedicineShelf"))
        {
            return Settings.Instance.ContainerMedicineShelfCapacity;
        }

        // First Aid Kit
        if (name.Contains("CONTAINER_FirstAidKit"))
        {
            return Settings.Instance.ContainerFirstAidCapacity;
        }

        // Infirmary Drawers
        if (name.Contains("OBJ_InfirmaryDrawer"))
        {
            return Settings.Instance.ContainerInfirmaryDrawerCapacity;
        }

        // Coal Bin
        if (name.Contains("CONTAINER_CoalBin"))
        {
            return Settings.Instance.ContainerCoalBinCapacity;
        }

        // Firewood Bin
        if (name.Contains("CONTAINER_FirewoodBin"))
        {
            return Settings.Instance.ContainerFirewoodBinCapacity;
        }

        // Supply Bin
        if (name.Contains("CONTAINER_ForestryCrate"))
        {
            return Settings.Instance.ContainerSupplyBinCapacity;
        }

        // Glove Box 1
        if (name.Contains("CarSedanGloveBox_Prefab"))
        {
            return Settings.Instance.ContainerGloveBoxCapacity;
        }

        // Glove Box 2
        if (name.Contains("CarTruckGloveBox_Prefab"))
        {
            return Settings.Instance.ContainerGloveBoxCapacity;
        }

        // Trunk (Car)
        if (name.Contains("CarSedanTrunkDoor_Prefab"))
        {
            return Settings.Instance.ContainerCarTrunkCapacity;
        }

        // Backpack
        if (name.Contains("CONTAINER_BackPack"))
        {
            return Settings.Instance.ContainerBackpackCapacity;
        }

        // Briefcase
        if (name.Contains("CONTAINER_Briefcase"))
        {
            return Settings.Instance.ContainerBriefcaseCapacity;
        }

        // Suitcase
        if (name.Contains("OBJ_Suitcase"))
        {
            return Settings.Instance.ContainerSuitcaseCapacity;
        }

        // Hidden Cache
        if (name.Contains("CONTAINER_CacheStoreCommon"))
        {
            return Settings.Instance.ContainerHiddenCacheCapacity;
        }

        // Plastic Container 1
        if (name.Contains("CONTAINER_CacheStoreRare"))
        {
            return Settings.Instance.ContainerPlasticContainerCapacity;
        }

        //Plastic Container
        if (name.Contains("CONTAINER_PlasticBox"))
        {
            return Settings.Instance.ContainerPlasticContainerCapacity;
        }

        // Metal Box
        if (name.Contains("CONTAINER_MetalBox"))
        {
            return Settings.Instance.ContainerMetalContainerCapacity;
        }

        // Gun Locker
        if (name.Contains("CONTAINER_StorageGunLocker"))
        {
            return Settings.Instance.ContainerGunLockerCapacity;
        }

        // Lock Box
        if (name.Contains("CONTAINER_LockBoxB"))
        {
            return Settings.Instance.ContainerLockBoxCapacity;
        }

        // Locker 1
        if (name.Contains("CONTAINER_LockerA"))
        {
            return Settings.Instance.ContainerLockerCapacity;
        }
       
        // Locker 2
        if (name.Contains("CONTAINER_MetalLocker"))
        {
            return Settings.Instance.ContainerLockerCapacity;
        }

        // Locker 3
        if (name.Contains("OBJ_MetalLockerDoor"))
        {
            return Settings.Instance.ContainerLockerCapacity;
        }

        // Safe
        if (name.Contains("CONTAINER_Safe"))
        {
            return Settings.Instance.ContainerSafeCapacity;
        }

        // Cash Register
        if (name.Contains("OBJ_CashRegisterDrawer"))
        {
            return Settings.Instance.ContainerCashRegisterCapacity;
        }

        // Safety Deposit Box
        if (name.Contains("STR_BankAVaultDepositBox"))
        {
            return Settings.Instance.ContainerSafetyDepositBoxCapacity;
        }

        // Hatch
        if (name.Contains("CONTAINER_StoneCabinATrapDoor"))
        {
            return Settings.Instance.ContainerHatchCapacity;
        }

        // Rock Cache
        if (name.Contains("GEAR_RockCache_Prefab"))
        {
            return Settings.Instance.ContainerRockCacheCapacity;
        }

        // Cargo Container
        if (name.Contains("OBJ_CargoCrateBottomDoor") || name.Contains("OBJ_CargoCrateTopDoor"))
        {
            return Settings.Instance.ContainerCargoContainerCapacity;
        }

        // IM ADDING MY STUFF HERE

        // Wooden veggie rack
        if (name.Contains("CONTAINER_WoodenVegRack"))
        {
            return Settings.Instance.ContainerWoodenVegRackCapacity;
        }

        // Rustic Closet doors A
        if (name.Contains("OBJ_Rustic_Closet_Door_A"))
        {
            if (HasAncestor(t, "CONTAINER_Rustic_Closet_B")) return Settings.Instance.ContainerRusticClosetBDoorTwoMediumDoorsCapacity;
            if (HasAncestor(t, "CONTAINER_Rustic_Closet_A")) return Settings.Instance.ContainerRusticClosetADoorTallLeftDoorCapacity;
            if (HasAncestor(t, "CONTAINER_Rustic_Closet_C")) return Settings.Instance.ContainerRusticCupboardCapacity;
        }

        // Rustic Closet doors B
        if (name.Contains("OBJ_Rustic_Closet_Door_B"))
        {
            if (HasAncestor(t, "CONTAINER_Rustic_Closet_B")) return Settings.Instance.ContainerRusticClosetBDoorTwoMediumDoorsCapacity;
            if (HasAncestor(t, "CONTAINER_Rustic_Closet_A")) return Settings.Instance.ContainerRusticClosetADoorShortRightDoorCapacity;
        }

        // Rustic closet drawers
        if (name.Contains("OBJ_Rustic_Closet_Drawer"))
        {
            if (HasAncestor(t, "CONTAINER_Rustic_Closet_B")) return Settings.Instance.ContainerRusticClosetBDrawerCapacity;
            if (HasAncestor(t, "CONTAINER_Rustic_Closet_A")) return Settings.Instance.ContainerRusticClosetADrawerCapacity;
        }

        //Display shelf drawers
        if (name.Contains("OBJ_Rustic_Display_Shelf_Drawer"))
        {
            return Settings.Instance.ContainerDisplayShelfDrawerCapacity;
        }

        //Dresser drawers`
        if (name.Contains("OBJ_Rustic_Dresser_Drawer"))
        { 
            if (HasAncestor(t, "CONTAINER_Rustic_Dresser_A")) return Settings.Instance.ContainerRusticDresserDrawerACapacity;
            if (HasAncestor(t, "CONTAINER_Rustic_End_Table")) return Settings.Instance.ContainerRusticEndTableCapacity;
            if (HasAncestor(t, "CONTAINER_Rustic_Dresser_B"))
            {
                if (name.Contains("OBJ_Rustic_Dresser_Drawer_B")) return Settings.Instance.ContainerRusticWideDresserSmallDrawerCapacity;
                if (name.Contains("OBJ_Rustic_Dresser_Drawer_C")) return Settings.Instance.ContainerRusticWideDresserLargeDrawerCapacity;
            }
        }   

        return null;
         }
    }
