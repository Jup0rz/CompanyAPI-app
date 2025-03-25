import { BrowserRouter, Route, Routes } from "react-router-dom"
import { List } from "./components/List"
import { NewCompany } from "./components/NewCompany"
import { EditCompany } from "./components/EditCompany"

function App() {
  return (
    <BrowserRouter>
      <Routes>
          <Route path="/" element={<List/>}/>
          <Route path="/newcompany" element={<NewCompany/>}/>
          <Route path="/editcompany/:id" element={<EditCompany/>}/>
      </Routes>
    </BrowserRouter>
  )
}

export default App
