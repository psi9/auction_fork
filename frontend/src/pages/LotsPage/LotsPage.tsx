import { useEffect, useState, useContext } from "react";

import { Lot } from "../../objects/Entities";

import { LotContext } from "../../contexts/LotContext";
import { AuctionContext } from "../../contexts/AuctionContext";

import LotPageContent from "./lotPageComponents/lotPageContent/LotPageContent";
import LotPageForm from "./lotPageComponents/lotPageForm/LotPageForm";
import LotPageHeader from "./lotPageComponents/lotPageHeader/LotPageHeader";

export default function LotsPage() {
  const [lots, setLots] = useState<Lot[] | undefined>([]);

  const { auction } = useContext(AuctionContext);
  const { getLotsByAuction } = useContext(LotContext);

  useEffect(() => {
    const getLots = async () => {
      setLots(await getLotsByAuction());
    };

    getLots();
  }, []);

  return (
    <div className="main_box">
      <LotPageHeader auction={auction!} />
      <LotPageForm auction={auction!} />
      <LotPageContent lots={lots!} />
    </div>
  );
}
