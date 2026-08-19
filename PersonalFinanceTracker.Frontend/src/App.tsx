import './App.css'
import {Button} from "@/components/ui/button.tsx";

function App() {
  return (
    <>
      <div className="flex flex-col h-25 justify-center items-center text-3xl my-3 mx-[40%] bg-yellow-300">
          <div>Привет, мир</div>
          <Button onClick={() => alert("Клик...")}>Клик</Button>
      </div>

    </>
  )
}

export default App
