<script setup lang="ts">
import {login} from "@/service/Client.ts";
import {useAuthStore} from "@/service/AuthStore.ts";
import {Button} from "@/components/ui/button";
import {Input} from "@/components/ui/input";

const authStore = useAuthStore()
</script>

<template>
  <div class="flex items-center justify-center flex-col gap-3">
      <div v-if="authStore.data == null">Вы не авторизованы...</div>
      <div v-else class="flex flex-col gap-3">
        <div>Вы авторизованы</div>
        <div><span>Токен: </span><Input :model-value="authStore.data.token" readonly></Input></div>
        <div><span>Имя: </span><Input :model-value="authStore.data.userName" readonly></Input></div>
        <div><span>Роль: </span><Input :model-value="authStore.data.role" readonly></Input></div>
      </div>
      <div class="flex flex-row gap-2">
        <Button @click="login()">Авторизация</Button>
        <Button @click="authStore.data = null">Выход</Button>
      </div>
  </div>
</template>
