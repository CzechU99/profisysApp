<template>
  <div class="login-page">
    <form @submit.prevent="login" class="login-form">
      <h2>Zaloguj się</h2>
      <input
        v-model="username"
        placeholder="Nazwa użytkownika"
        required
        class="input-field"
      />
      <input
        type="password"
        v-model="password"
        placeholder="Hasło"
        required
        class="input-field"
      />
      <button type="submit" class="login-button">Zaloguj</button>
    </form>
  </div>
</template>

<script>
import { ref } from 'vue'
import { useAuthStore } from '../stores/authentication'
import { useToast } from 'vue-toastification'


export default {
  setup() {
    const auth = useAuthStore()
    const username = ref('')
    const password = ref('')
    const toast = useToast()


    const login = async () => {
      try {
        await auth.login(username.value, password.value)
        toast.success('Zalogowano pomyślnie.')
      } catch (error) {
        toast.error('Brak użytkownika o takiej nazwie lub haśle.')
      }
    }

    return { username, password, login }
  }
}
</script>

<style scoped>
.login-page {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
  margin-top: 300px;
}

.login-form {
  background: white;
  padding: 2.5rem;
  border-radius: 12px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
  width: 320px;
  transition: transform 0.2s;
}

.login-form h2 {
  text-align: center;
  color: #333;
}

.input-field {
  padding: 0.8rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  transition: border-color 0.2s, box-shadow 0.2s;
}

.input-field:focus {
  border-color: #4facfe;
  box-shadow: 0 0 5px rgba(79, 172, 254, 0.5);
  outline: none;
}

.login-button {
  background-color: #4facfe;
  color: white;
  padding: 0.9rem;
  border: none;
  border-radius: 8px;
  font-weight: bold;
  cursor: pointer;
  transition: background-color 0.2s, transform 0.2s;
}

.login-button:hover {
  background-color: #77c0ff;
  transform: translateY(-2px);
}
</style>
