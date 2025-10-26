<template>
  <Button 
    label="Wyloguj" 
    icon="pi pi-sign-out" 
    class="btn-logout"
    severity="danger"
    outlined
    @click="logout"
  />
</template>

<script setup>
import { useAuthStore } from '../../stores/authStore'

const authStore = useAuthStore()
const logout = async () => await authStore.logout()
</script>
