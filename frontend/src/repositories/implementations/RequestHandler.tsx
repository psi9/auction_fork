import { enqueueSnackbar } from "notistack";

export function handleCommonResponse(response: Response): boolean {
  if (response.status === 401) {
    enqueueSnackbar("Необходимо авторизоваться", {
      variant: "warning",
    });
    return false;
  }

  if (!response.ok) {
    enqueueSnackbar("Что-то пошло не так, попробуйте снова", {
      variant: "warning",
    });
    return false;
  }

  return true;
}

export async function handleCommonRequest<T>(
  url: string,
  httpMethod: string,
  body: any
): Promise<T | undefined> {
  const response = await fetch(url, {
    method: httpMethod,
    headers: {
      "Content-Type": "application/json; charset: UTF-8;",
    },
    body: JSON.stringify(body),
    credentials: "include",
  });

  if (!handleCommonResponse(response)) return;

  const data = await response.json();

  return data;
}
