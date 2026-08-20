import './App.css'
import {Button} from "@/components/ui/button.tsx";
import {create} from "zustand/react";
import {persist} from "zustand/middleware";
import {useAuthStore} from "@/service/AuthStore.ts";

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
      <div className="flex flex-col h-50 justify-center items-center text-3xl my-3 mx-[40%] bg-yellow-300">
          <div>Привет, мир</div>
          <Button onClick={increasePopulation}>Клик</Button>
          {bears}
          <Button onClick={() => setData(null)}>Выход</Button>
      </div>

    </>
  )
}

export default App
