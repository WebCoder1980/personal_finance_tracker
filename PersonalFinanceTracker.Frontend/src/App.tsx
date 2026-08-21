import './App.css'
import {Button} from "@/components/ui/button.tsx";
import {create} from "zustand/react";
import {persist} from "zustand/middleware";
import {useAuthStore} from "@/service/AuthStore.ts";
import {Card, CardContent, CardFooter} from "@/components/ui/card.tsx";

type BearStore = {
    bears: number,
    increasePopulation: () => void,
    removeAllBears: () => void,
    updateBears: (newBears : number) => void
}

const useBear = create<BearStore>()(
    persist(
        (set) => ({
        bears: 0,
        increasePopulation: () => set((state) => ({ bears: state.bears + 1 })),
        removeAllBears: () => set({ bears: 0 }),
        updateBears: (newBears) => set({ bears: newBears }),
        }),
        {
            name: 'bear-storage'
        }
    )
)

function App() {
    const { bears, increasePopulation } = useBear();
    const { setData } = useAuthStore();

    return (
    <>
        <div className="flex min-h-screen justify-center items-center">
          <Card className="w-full max-w-sm">
              <CardContent className="flex flex-col">
                  Привет, мир
                  <Button onClick={increasePopulation}>Клик</Button>
                  Counter: {bears}
              </CardContent>
              <CardFooter>
                  <Button variant="outline" onClick={() => setData(null)} className="w-full">Выход</Button>
              </CardFooter>
          </Card>
        </div>
    </>
  )
}

export default App
