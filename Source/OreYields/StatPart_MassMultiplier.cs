using Verse;
using RimWorld;

namespace OreYields
{
  public class StatPart_MassMultiplier : StatPart
  {
    float multiplier = 1f;

    public override void TransformValue(StatRequest req, ref float val)
    {
      // I know this is shit, leave me alone
      if (req.Thing.def.defName == "Silver")
      {
        multiplier = OreYieldsSettings.multiplySilverMass;
      }
      else if (req.Thing.def.defName == "Gold")
      {
        multiplier = OreYieldsSettings.multiplyGoldMass;
      } 
      else if (req.Thing.def.defName == "Steel")
      {
        multiplier = OreYieldsSettings.multiplySteelMass;
      }
      else if (req.Thing.def.defName == "Plasteel")
      {
        multiplier = OreYieldsSettings.multiplyPlasteelMass;
      }
      else if (req.Thing.def.defName == "Uranium")
      {
        multiplier = OreYieldsSettings.multiplyUraniumMass;
      }
      else if (req.Thing.def.defName == "Jade")
      {
        multiplier = OreYieldsSettings.multiplyJadeMass;
      }
      else if (req.Thing.def.defName == "ComponentIndustrial")
      {
        multiplier = OreYieldsSettings.multiplyCompsMass;
      }

      val *= multiplier;
    } 

    public override string ExplanationPart(StatRequest req)
    {
      return "MassDescription".Translate() + ": x" + multiplier.ToStringPercent();
    }
  }
}
