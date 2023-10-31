import { Lot } from "../../../objects/Entities";

import "./LotCard.css";
import { getStateFromEnum } from "../../../objects/Enums";

export default function LotCard(props: { lot: Lot }) {
  const buyoutLot =
    props.lot.buyoutPrice === 0 ? "Не выкуплен" : `${props.lot.buyoutPrice}р.`;

  return (
    <div className="card_container">
      <div className="title">{props.lot.name}</div>
      <div className="state">Статус: {getStateFromEnum(props.lot.state)}</div>
      <div className="description">{props.lot.description}</div>
      <div className="image_box">
        {props.lot.images.map((image, index) => (
          <div key={index}>
            <img
              className="image"
              src={`data:image/jpeg;base64, ${image.data}`}
              alt={image.name}
            />
          </div>
        ))}
      </div>
      <div className="container_info_lot">
        <div>
          <div className="info_price">Начальная цена:</div>
          <div className="price_text">{props.lot.startPrice}p.</div>
          <div className="info_price">Текущая цена:</div>
          <div className="price_text">{props.lot.startPrice}p.</div>
        </div>
        <div>
          <div className="info_price">Цена выкупа:</div>
          <div className="price_text">{buyoutLot}</div>
          <div className="info_price">Шаг ставки:</div>
          <div className="price_text">{props.lot.betStep}p.</div>
        </div>
      </div>
      <button className="submit_create">Сделать ставку</button>
    </div>
  );
}
