<script setup lang="ts">
import {useAuthStore} from "@/service/AuthStore.ts";
import {register} from "@/service/Client.ts";
import {ref} from "vue";
import {useRouter} from "vue-router";
import {Button} from "@/components/ui/button";
import {Alert, AlertDescription, AlertTitle} from "@/components/ui/alert";
import {Input} from "@/components/ui/input";

const router = useRouter()
const authStore = useAuthStore()
if (authStore.data != null) {
  router.push("/transactions")
}
const curLogin = ref("")
const curPassword = ref("")
const curPassword2 = ref("")
const curError = ref("")

async function registerButtonClicked() {
  if (curPassword.value != curPassword2.value) {
    curError.value = "Пароли должны совпадать."
    return
  }

  try {
    await register(curLogin.value, curPassword.value);
  }
  catch (error) {
    console.log(error)
    curError.value = "Проверьте корректность логина или пароля. Минимум 5 символов"
    return
  }
  router.push('/login')
}
</script>

<template>
  <div class="flex items-center justify-center min-h-screen flex-col gap-3 px-[40%]">
    <div>Зарегистрироваться</div>
    <div class="flex flex-col w-full">
      <span>Логин: </span>
      <Input v-model="curLogin"></Input>
    </div>
    <div class="flex flex-col w-full">
      <span>Пароль: </span>
      <Input type="password" v-model="curPassword"></Input></div>
    <div class="flex flex-col w-full">
      <span>Повторите пароль: </span>
      <Input type="password" v-model="curPassword2"></Input>
    </div>
    <Button @click="async () => await registerButtonClicked()">Регистрация</Button>
    <Alert variant="destructive" v-if="curError != ''">
      <AlertTitle>Ошибка при входе!</AlertTitle>
      <AlertDescription>
        {{curError}}
      </AlertDescription>
    </Alert>
    <RouterLink to="/login">Уже есть аккаунт?</RouterLink>
  </div>
</template>