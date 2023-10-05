import { Auction, User } from "../../../objects/Entities";

import "./UserCard.css";

export default function UserCard(props: {
  user: User;
  auction: Auction | undefined;
}) {
  const user = require("./assets/user.png");

  return (
    <div className="card_container">
      <div className="container_title">
        <img className="user_img" src={user} alt="Профиль" />
        <div className="name">{props.user.name}</div>
      </div>
      <div className="container_info_user">
        <div className="info_auctions">Организатор:</div>
        <div className="auction_name">{props.auction?.name}</div>
      </div>
    </div>
  );
}
