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
  const { curAuctionId } = useContext(LotContext);
  const { getAuction, deleteAuction } = useContext(AuctionContext);

  const [lots, setLots] = useState<Lot[] | undefined>([]);
  const [auction, setAuction] = useState<Auction>();

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [startPrice, setStartPrice] = useState<number>();
  const [betStep, setBetStep] = useState<number>();

  const { members, user } = useContext(UserAuthorizationContext);
  const author = members?.find((member) => member.id === auction?.authorId);
  const isAuthor = user?.id === author?.id;

  const [selectedImages, setSelectedImages] = useState<FileList | null>(null);

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
    setSelectedImages(null);
  };

  const createNewLot = () => {
    if (!validateCreateLot()) return;

    const lot: Lot = {
      id: "",
      name: title,
      description: description,
      auctionId: curAuctionId,
      startPrice: startPrice!,
      buyoutPrice: 0,
      betStep: betStep!,
      state: State.awaiting,
      bets: [],
      images: [],
    };

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

  const handleImageChange = (event: ChangeEvent<HTMLInputElement>) => {
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
              {new Date(auction?.dateStart!).toLocaleDateString()}
            </div>
          </div>
          <div className="wrapper">
            <div className="sub_title">Конец:</div>
            <div className="date_end">
              {new Date(auction?.dateEnd!).toLocaleDateString()}
            </div>
          </div>
        </div>
        {isAuthor && (
          <button className="dismiss_btn" onClick={deleteCurAuction}>
            <img className="dismiss_image" alt="Dismiss" />
          </button>
        )}
      </div>
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
                accept="image"
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
