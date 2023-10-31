import { enqueueSnackbar } from "notistack";

export function handleCommonResponse(response: Response): boolean {
  if (response.status === 401) {
    enqueueSnackbar("Необходимо авторизоваться", {
      variant: "warning",
    });
    return false;
  }

  return true;
}

export async function handleCommonRequest(
  url: string,
  httpMethod: string,
  body: any
): Promise<void> {
  const response = await fetch(url, {
    method: httpMethod,
    headers: {
      "Content-Type": "application/json; charset: UTF-8;",
    },
    body: JSON.stringify(body),
    credentials: "include",
  });

  handleCommonResponse(response);
}
