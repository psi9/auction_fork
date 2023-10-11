import LotCard from "../../components/cards/lotCard/LotCard";

import "./LotsPage.css";

import { useLotContext } from "../../contexts/LotContext";
import { useNavigate } from "react-router-dom";
import { useUserAuthorityContext } from "../../contexts/UserAuthorityContext";
import { useEffect, useState } from "react";
import { Lot } from "../../objects/Entities";

export default function LotsPage() {
  const userAuthorityContext = useUserAuthorityContext();
  const navigate = useNavigate();

  useEffect(() => {
    if (!userAuthorityContext?.checkAccess()) navigate("/authority");
  });

  const lotContext = useLotContext();

  useEffect(() => {
    async function getLots() {
      setLots(await lotContext?.getLotsByAuction()!);
    }

    getLots();
  });

  const [error, setError] = useState<string>("");
  const [lots, setLots] = useState<Lot[]>([]);

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");

  return (
    <div className="main_box">
      <div className="input_box">
        <div className="title_create">Создайте лот</div>
        <input
          className="create_name"
          type="text"
          value={title}
          onChange={(event) => setTitle(event.target.value)}
          placeholder="Введите название лота (до 30 символов)"
        />
        <textarea
          className="create_description"
          rows={10}
          value={description}
          onChange={(event) => setDescription(event.target.value)}
          placeholder="Введите описание лота (до 300 символов)"
        ></textarea>
        <button className="submit_create">Создать</button>
        <div className="error">{error}</div>
      </div>
      <div className="main_container">
        {!lots?.length ? (
          <div className="main_empty">
            <div className="empty">
              <div>Лотов пока нет.</div>
            </div>
          </div>
        ) : (
          lots.map((lot) => <LotCard key={lot.id} lot={lot} />)
        )}
      </div>
    </div>
  );
}
