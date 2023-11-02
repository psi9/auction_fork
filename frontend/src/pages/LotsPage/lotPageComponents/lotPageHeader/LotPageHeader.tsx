import { useContext, useState } from "react";

import "./LotPageHeader.css";

import { enqueueSnackbar } from "notistack";

import { AuctionContext } from "../../../../contexts/AuctionContext";
import { Auction } from "../../../../objects/Entities";
import { State, getStateFromEnum } from "../../../../objects/Enums";

export default function LotPageHeader(props: { auction: Auction }) {
  const { author, isAuthor } = useContext(AuctionContext);
  const [open, setOpen] = useState(false);

  const { deleteAuction, changeState, curAuctionId } =
    useContext(AuctionContext);

  const isChangable =
    props.auction?.state == State.running ||
    props.auction?.state == State.editing ||
    props.auction?.state == State.awaiting;

  const isEndValid = props.auction?.dateStart < props.auction?.dateEnd;

  const handleOpen = () => {
    setOpen(!open);
  };

  const changeStateCurAuction = async (state: State) => {
    if (!curAuctionId) return;

    if (props.auction?.state == state) {
      enqueueSnackbar("Выберите статус отличный от текущего", {
        variant: "warning",
      });
      return;
    }

    await changeState(curAuctionId, state);
  };

  const deleteCurAuction = async () => {
    if (!curAuctionId) return;
    await deleteAuction(curAuctionId);
  };

  return (
    <div>
      <div className="about_box">
        <div className="main_information">
          <div className="about_title">{props.auction?.name}</div>
          <div className="about_description">{props.auction?.description}</div>
        </div>
        <div className="adding_info">
          <div className="wrapper">
            <div className="sub_title">Статус:</div>
            <div className="info_status">
              {getStateFromEnum(props.auction?.state!)}
            </div>
          </div>
          <div className="wrapper">
            <div className="sub_title">Автор:</div>
            <div className="info_owner">{author?.name}</div>
          </div>
          <div className="wrapper">
            <div className="sub_title">Начало:</div>
            <div className="date_start">
              {new Date(props.auction?.dateStart!).toLocaleString()}
            </div>
          </div>
          {isEndValid && (
            <div className="wrapper">
              <div className="sub_title">Конец:</div>

              <div className="date_end">
                {new Date(props.auction?.dateEnd!).toLocaleString()}
              </div>
            </div>
          )}
        </div>
        {isAuthor && (
          <div className="button_auction_box">
            {isChangable && (
              <div className="button_auction_box">
                <button className="button_item" onClick={handleOpen}>
                  <img
                    className="image_item change_status"
                    alt="Изменить статус"
                  />
                </button>
                {open ? (
                  <div className="menu">
                    <button
                      className="button_item"
                      onClick={() => changeStateCurAuction(State.awaiting)}
                    >
                      Ожидание
                    </button>
                    <button
                      className="button_item"
                      onClick={() => changeStateCurAuction(State.editing)}
                    >
                      Редактирование
                    </button>
                    <button
                      className="button_item"
                      onClick={() => changeStateCurAuction(State.running)}
                    >
                      Запущен
                    </button>
                    <button
                      className="button_item"
                      onClick={() => changeStateCurAuction(State.completed)}
                    >
                      Завершен
                    </button>
                    <button
                      className="button_item"
                      onClick={() => changeStateCurAuction(State.canceled)}
                    >
                      Отменен
                    </button>
                  </div>
                ) : null}
                {/* <button className="button_item">
                  <img className="image_item edit" alt="Редактировать" />
                </button> */}
                <button
                  className="button_item danger_button"
                  onClick={() => changeStateCurAuction(State.completed)}
                >
                  <img className="image_item completed" alt="Завершить" />
                </button>
              </div>
            )}
          </div>
        )}
      </div>
      {isAuthor && (
        <div className="help_div">
          <button
            className="button_item danger_button danger_delete_button"
            onClick={deleteCurAuction}
          >
            <img className="image_item delete" alt="Удалить" />
          </button>
        </div>
      )}
    </div>
  );
}
