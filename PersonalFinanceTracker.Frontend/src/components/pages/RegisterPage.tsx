import { zodResolver } from "@hookform/resolvers/zod"
import * as z from "zod"
import {Controller, useForm} from "react-hook-form";
import {Field, FieldError, FieldGroup, FieldLabel} from "../ui/field";
import { Input } from "../ui/input";
import {Button} from "@/components/ui/button.tsx";
import {register} from "@/service/Client.ts";
import {useState} from "react";
import {Alert, AlertDescription, AlertTitle} from "@/components/ui/alert.tsx";

export const formSchema = z.object({
    login: z
        .string()
        .min(5, "Логин должен быть больше 5 символов")
        .max(50, "Логин должен быть меньше 50 символов"),
    password: z
        .string()
        .min(5, "Пароль должен быть больше 5 символов")
        .max(50, "Пароль должен быть меньше 50 символов"),
    passwordRepeat: z
        .string()
        .min(5, "Повторение пароля должно быть больше 5 символов")
        .max(50, "Повторение пароля должно быть меньше 50 символов"),
}).refine((data) => data.password === data.passwordRepeat, {
    error: "Пароли должны совпадать",
    path: ["passwordRepeat"]
})

export function RegisterPage() {
    const [isErrorVisible, setIsErrorVisible] = useState<boolean>(false)

    async function onSubmit(data : z.infer<typeof formSchema>) {
        try {
            await register(data.login, data.password)
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
            password: "",
            passwordRepeat: ""
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
                    <Controller
                        name="passwordRepeat"
                        control={form.control}
                        render={({ field, fieldState }) => (
                            <Field data-invalid={fieldState.invalid}>
                                <FieldLabel>Повторение пароля</FieldLabel>
                                <Input type="password" value={field.value} onChange={field.onChange} aria-invalid={fieldState.invalid}></Input>
                                {fieldState.invalid && (
                                    <FieldError errors={[fieldState.error]} />
                                )}
                            </Field>
                        )}
                    />
                    <Field>
                        <Button type="submit">
                            Зарегистрироваться
                        </Button>
                    </Field>
                    {isErrorVisible && <Alert variant="destructive">
                        <AlertTitle>Ошибка при регистрации!</AlertTitle>
                        <AlertDescription>
                            Регистрация не удалась, попробуйте позже
                        </AlertDescription>
                    </Alert>}
                </FieldGroup>
            </form>
            <Button variant="link">
                <a href="/login">Аккаунт уже существует?</a>
            </Button>
        </div>
    </>
}