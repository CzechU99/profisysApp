import { useToast } from "vue-toastification";

const toast = useToast();

export function handleApiError(error) {
  if (error.response) {
    const data = error.response.data;
    const status = error.response.status;

    if (data.errors) {
      const messages = Object.values(data.errors).flat().join("\n");
      toast.error(messages);
    }
    else if (data.message) {
      toast.error(data.message);
    }
    else if (status === 403) {
      toast.error("Nie masz uprawnień do tej operacji!");
    } else if (status === 401) {
      toast.error("Musisz się zalogować!");
    }
    else {
      toast.error(`Błąd serwera (${status})`);
    }
  } else if (error.request) {
    toast.error("Brak odpowiedzi z serwera!");
  } else {
    toast.error("Nieznany błąd aplikacji!");
  }
}
