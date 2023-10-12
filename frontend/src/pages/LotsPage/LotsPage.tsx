import LotCard from "../../components/cards/lotCard/LotCard";

import "./LotsPage.css";

import { useLotContext } from "../../contexts/LotContext";
import { useEffect, useState } from "react";
import { Lot } from "../../objects/Entities";

export default function LotsPage() {
  const lotContext = useLotContext();

  const [error, setError] = useState<string>("");
  const [lots, setLots] = useState<Lot[]>([]);

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [startPrice, setStartPrice] = useState<number>(100);
  const [betStep, setBetStep] = useState<number>(100);

  useEffect(() => {
    async function getLots() {
      setLots(await lotContext?.getLotsByAuction()!);
    }

    getLots();
  }, [lotContext]);

  const createLot = () => {};

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
        <div className="box_inner">
          <input
            className="inner_item"
            type="text"
            value={startPrice}
            onChange={(event) => {
              const value = event.target.value;

              if (isNaN(+value)) {
                setError("Поле должно содержать только числовое значение");
                return;
              }

              if (+value > 1000000000) {
                setError("Слишком большое значение");
                return;
              }

              setError("");
              setStartPrice(+value);
            }}
            placeholder="Введите стартовую цену"
          />
          <input
            className="inner_item"
            type="number"
            value={betStep}
            onChange={(event) => {
              const value = event.target.value;

              if (isNaN(+value)) {
                setError("Поле должно содержать только числовое значение");
                return;
              }

              if (+value > 1000000000) {
                setError("Слишком большое значение");
                return;
              }

              setError("");
              setBetStep(+value);
            }}
            placeholder="Введите шаг ставки лота"
          />
        </div>
        <button className="submit_create" onClick={createLot}>
          Создать
        </button>
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
