import { zodResolver } from "@hookform/resolvers/zod"
import * as z from "zod"
import {Controller, useForm} from "react-hook-form";
import {Field, FieldError, FieldGroup, FieldLabel} from "../ui/field";
import { Input } from "../ui/input";
import {Button} from "@/components/ui/button.tsx";
import {login} from "@/service/Client.ts";
import {useState} from "react";
import {Alert, AlertDescription, AlertTitle} from "@/components/ui/alert.tsx";
import {useAuthStore} from "@/service/AuthStore.ts";

export const formSchema = z.object({
    login: z
        .string()
        .min(5, "Логин должен быть больше 5 символов")
        .max(50, "Логин должен быть меньше 50 символов"),
    password: z
        .string()
        .min(5, "Пароль должен быть больше 5 символов")
        .max(50, "Пароль должен быть меньше 50 символов"),
})

export function LoginPage() {
    const [isErrorVisible, setIsErrorVisible] = useState<boolean>(false)
    const {setData} = useAuthStore()

    async function onSubmit(data : z.infer<typeof formSchema>) {
        try {
            const authData = await login(data.login, data.password)
            setData(authData)
        }
        catch (error) {
            console.log(error)
            setIsErrorVisible(true)
        }
    }

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            login: "",
            password: ""
        }
    })

    return <>
        <div className="flex flex-col mx-[40%] mt-10">
            <form onSubmit={form.handleSubmit(onSubmit)}>
                <FieldGroup>
                    <Controller
                        name="login"
                        control={form.control}
                        render={({ field, fieldState }) => (
                                <Field data-invalid={fieldState.invalid}>
                                    <FieldLabel>Логин</FieldLabel>
                                    <Input value={field.value} onChange={field.onChange} aria-invalid={fieldState.invalid}></Input>
                                    {fieldState.invalid && (
                                        <FieldError errors={[fieldState.error]} />
                                    )}
                                </Field>
                            )}
                        />
                    <Controller
                        name="password"
                        control={form.control}
                        render={({ field, fieldState }) => (
                            <Field data-invalid={fieldState.invalid}>
                                <FieldLabel>Пароль</FieldLabel>
                                <Input type="password" value={field.value} onChange={field.onChange} aria-invalid={fieldState.invalid}></Input>
                                {fieldState.invalid && (
                                    <FieldError errors={[fieldState.error]} />
                                )}
                            </Field>
                        )}
                    />
                    <Field>
                        <Button type="submit">
                            Ввойти
                        </Button>
                    </Field>
                    {isErrorVisible && <Alert variant="destructive">
                        <AlertTitle>Ошибка при входе!</AlertTitle>
                        <AlertDescription>
                            Проверьте правильность логина или пароля.
                        </AlertDescription>
                    </Alert>}
                </FieldGroup>
            </form>
            <Button variant="link">
                <a href="/register">Ещё нет аккаунта?</a>
            </Button>
        </div>
    </>
}