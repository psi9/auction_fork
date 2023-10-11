import { useNavigate } from "react-router-dom";
import { useUserAuthorityContext } from "../../contexts/UserAuthorityContext";
import "./ProfilePage.css";
import { useEffect } from "react";
import { useLotContext } from "../../contexts/LotContext";
import LotCard from "../../components/cards/lotCard/LotCard";
import { useAuctionContext } from "../../contexts/AuctionContext";
import AuctionCard from "../../components/cards/auctionCard/AuctionCard";
import { Auction, User } from "../../objects/Entities";
import { useUserContext } from "../../contexts/UserContext";

export default function ProfilePage() {
  const userAuthorityContext = useUserAuthorityContext();
  const navigate = useNavigate();

  useEffect(() => {
    if (!userAuthorityContext?.checkAccess()) navigate("/authority");
  });

  const signout = userAuthorityContext?.signout;
  const user = userAuthorityContext?.user;
  const lots = useLotContext()?.lots;

  const auctions = useAuctionContext()?.auctions.filter(
    (auction) => auction.authorId === user?.id
  );

  const users = useUserContext();

  const userImage = require("../../components/header/assets/user.png");

  return (
    <div className="profile_container">
      <div className="main_info">
        <img
          className="profile_image"
          src={userImage}
          alt="Изображение профиля"
        />
        <div className="profile_data">
          <div className="user_name">{user?.name}</div>
          <div className="user_email">{user?.email}</div>
        </div>
        <div className="button_box">
          <button className="signout_button" onClick={signout}>
            Выйти
          </button>
        </div>
      </div>
      <div className="activity">
        <div className="activity_box">
          <div className="box_title">Ваше участие</div>
          <div className="box_items">
            {!lots?.length ? (
              <div className="main_empty">
                <div className="empty">
                  <div>Вы еще не делали ставок!</div>
                </div>
              </div>
            ) : (
              lots.map((lot) => <LotCard key={lot.id} lot={lot} />)
            )}
          </div>
        </div>
        <div className="activity_box">
          <div className="box_title">Ваши аукционы</div>
          <div className="box_items">
            {!auctions?.length ? (
              <div className="main_empty">
                <div className="empty">
                  <div>Вы пока не организовывали аукционы</div>
                  <div>Пора начинать!</div>
                </div>
              </div>
            ) : (
              auctions.map((auction: Auction) => (
                <AuctionCard
                  key={auction.id}
                  auction={auction}
                  author={
                    users.find((user: User) => user.id === auction.authorId)!
                  }
                />
              ))
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
