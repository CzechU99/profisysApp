<template>
  <div class="login-page">
    <form @submit.prevent="login" class="login-form">
      <h2>Zaloguj się</h2>
      <input
        v-model="username"
        placeholder="Nazwa użytkownika"
        required
        class="input-field"
        autocomplete="username"
      />
      <input
        type="password"
        v-model="password"
        placeholder="Hasło"
        required
        class="input-field"
        autocomplete="current-password"
      />
      <button type="submit" class="login-button">Zaloguj</button>
    </form>
  </div>
</template>

<script>
import { ref } from 'vue'
import { useAuthStore } from '../stores/authStore'

export default {
  setup() {
    const auth = useAuthStore()
    const username = ref('')
    const password = ref('')

    const login = async () => {
      await auth.login(username.value, password.value)
    }

    return { username, password, login }
  }
}
</script>