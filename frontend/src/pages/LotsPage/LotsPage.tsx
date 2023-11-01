import LotCard from "../../components/cards/lotCard/LotCard";

import "./LotsPage.css";

import { useEffect, useState, useContext, ChangeEvent } from "react";
import { Auction, Lot } from "../../objects/Entities";
import { LotContext } from "../../contexts/LotContext";
import { AuctionContext } from "../../contexts/AuctionContext";
import { State, getStateFromEnum } from "../../objects/Enums";
import { UserAuthorizationContext } from "../../contexts/UserAuthorizationContext";
import { enqueueSnackbar } from "notistack";

export default function LotsPage() {
  const { getLotsByAuction, createLot } = useContext(LotContext);
  const { getAuction, deleteAuction, changeState, curAuctionId } =
    useContext(AuctionContext);

  const [lots, setLots] = useState<Lot[] | undefined>([]);
  const [auction, setAuction] = useState<Auction>();

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [startPrice, setStartPrice] = useState<number>();
  const [betStep, setBetStep] = useState<number>();

  const { members, user } = useContext(UserAuthorizationContext);
  const author = members?.find((member) => member.id === auction?.authorId);

  const isAuthor = user?.id === author?.id;
  const isChangable =
    auction?.state == State.running ||
    auction?.state == State.editing ||
    auction?.state == State.awaiting;

  const [selectedImages, setSelectedImages] = useState<FileList | undefined>(
    undefined
  );
  const [open, setOpen] = useState(false);

  useEffect(() => {
    const getLots = async () => {
      setLots(await getLotsByAuction());
    };

    getLots();
  }, []);

  useEffect(() => {
    if (!curAuctionId) return;

    const getCurAuctionAsync = async () => {
      setAuction(await getAuction(curAuctionId));
    };

    getCurAuctionAsync();
  }, [curAuctionId]);

  const resetState = () => {
    setTitle("");
    setDescription("");
    setStartPrice(0);
    setBetStep(0);
    setSelectedImages(undefined);
  };

  const createNewLot = () => {
    if (!validateCreateLot()) return;

    const formData = new FormData();

    for (const image of selectedImages!) {
      formData.append("images", image);
    }

    formData.append("name", title);
    formData.append("description", description);
    formData.append("auctionId", curAuctionId);
    formData.append("startPrice", startPrice?.toString()!);
    formData.append("betStep", betStep?.toString()!);

    createLot(formData);
    resetState();
  };

  const deleteCurAuction = async () => {
    if (!curAuctionId) return;
    await deleteAuction(curAuctionId);
  };

  const changeStateCurAuction = async (state: State) => {
    if (!curAuctionId) return;

    if (auction?.state == state) {
      enqueueSnackbar("Выберите статус отличный от текущего", {
        variant: "warning",
      });
      return;
    }

    await changeState(curAuctionId, state);
  };

  const handleOpen = () => {
    setOpen(!open);
  };

  const handleImageChange = async (event: ChangeEvent<HTMLInputElement>) => {
    const files = event.target.files;
    if (!files) return;

    if (files.length > 5) {
      enqueueSnackbar("Изображений может быть не более 5", {
        variant: "warning",
      });
      return;
    }

    setSelectedImages(files);
  };

  const validateCreateLot = (): boolean => {
    if (!title || !description || !startPrice || !betStep || !selectedImages) {
      enqueueSnackbar("Заполните все данные", {
        variant: "error",
      });
      return false;
    }
    return true;
  };

  return (
    <div className="main_box">
      <div className="about_box">
        <div className="main_information">
          <div className="about_title">{auction?.name}</div>
          <div className="about_description">{auction?.description}</div>
        </div>
        <div className="adding_info">
          <div className="wrapper">
            <div className="sub_title">Статус:</div>
            <div className="info_status">
              {getStateFromEnum(auction?.state!)}
            </div>
          </div>
          <div className="wrapper">
            <div className="sub_title">Автор:</div>
            <div className="info_owner">{author?.name}</div>
          </div>
          <div className="wrapper">
            <div className="sub_title">Начало:</div>
            <div className="date_start">
              {new Date(auction?.dateStart!).toLocaleString()}
            </div>
          </div>
          <div className="wrapper">
            <div className="sub_title">Конец:</div>
            <div className="date_end">
              {new Date(auction?.dateEnd!).toLocaleString()}
            </div>
          </div>
        </div>
        {isAuthor && (
          <div className="button_auction_box">
            {true && (
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
                <button className="button_item">
                  <img className="image_item edit" alt="Редактировать" />
                </button>
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
      {isAuthor && (
        <div className="input_box">
          <div className="title_create">Создайте лот</div>
          <input
            className="create_name"
            type="text"
            maxLength={30}
            value={title}
            onChange={(event) => setTitle(event.target.value)}
            placeholder="Введите название лота (до 30 символов)"
          />
          <textarea
            className="create_description"
            rows={10}
            maxLength={300}
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
                if (isNaN(+event.target.value)) return;
                if (+event.target.value.startsWith("0")) return;
                if (+event.target.value.length > 9) return;

                setStartPrice(+event.target.value);
              }}
              placeholder="Введите стартовую цену"
            />
            <input
              className="inner_item"
              type="number"
              value={betStep}
              onChange={(event) => {
                if (isNaN(+event.target.value)) return;
                if (+event.target.value.startsWith("0")) return;
                if (+event.target.value.length > 9) return;

                setBetStep(+event.target.value);
              }}
              placeholder="Введите шаг ставки лота"
            />
          </div>
          <div className="photo_box">
            <div className="input__wrapper">
              <input
                name="file"
                type="file"
                id="input__file"
                className="input input__file"
                multiple
                accept="image/jpeg"
                onChange={handleImageChange}
              />
              <label htmlFor="input__file" className="input__file-button">
                <span className="input__file-icon-wrapper">
                  <img
                    className="input__file-icon"
                    alt="Выбрать файл"
                    width="25"
                  />
                </span>
                <span className="input__file-button-text">
                  Выберите изображения
                </span>
              </label>
            </div>
            <div>
              {selectedImages && (
                <div className="image_container">
                  <p className="about_image">Выбранные изображения</p>
                  <ul className="image_wrapper">
                    {Array.from(selectedImages).map((image, index) => (
                      <img
                        className="image"
                        key={index}
                        src={URL.createObjectURL(image)}
                        alt="Выбранные изображения"
                      />
                    ))}
                  </ul>
                </div>
              )}
            </div>
          </div>
          <button className="submit_create" onClick={createNewLot}>
            Создать
          </button>
        </div>
      )}
      <div className="main_container_lots">
        {!lots?.length ? (
          <div className="main_empty">
            <div className="empty">
              <div>Лотов пока нет.</div>
            </div>
          </div>
        ) : (
          lots.map((lot) => <LotCard key={lot.id} lot={lot} />).reverse()
        )}
      </div>
    </div>
  );
}
