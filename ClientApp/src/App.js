
import "bootstrap/dist/css/bootstrap.min.css"
import { useEffect, useState } from "react";

const App = () => {

    const [tasks, setTasks] = useState([]);
    const [description, setDescription] = useState("");

    const getTasks = async () => {
        const response = await fetch("api/Tareas");
        if (response.ok) {
            const data = await response.json();
            setTasks(data);
        }
        else {
            console.log("status code " + response.status);
        }
    }

    useEffect(() => {
        getTasks()
    }, []);


    const saveTask = async (e) => {
        e.preventDefault()

        const response = await fetch("api/Tareas", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ description: description })
        })

        if (response.ok) {
            setDescription("");
            await getTasks();
        }
    }

    const closeTask = async (id) => {

        const response = await fetch("api/tasks/Delete/" + id, {
            method: "DELETE"
        })

        if (response.ok) {
            await getTasks();
        }
    }

    const formatDate = (string) => {
        let options = { year: 'numeric', month: 'long', day: 'numeric' };
        let fecha = new Date(string).toLocaleDateString("es-PE", options);
        let hora = new Date(string).toLocaleTimeString();
        return fecha + " | " + hora
    }

    return (
        <div className="container bg-dark p-4 vh-100">

            <h2 className="text-white">Tasks</h2>
            <div className="row">
                <div className="col-sm-12">
                    <form onSubmit={saveTask}>

                        <div className="input-group">
                            <input type="text"
                                className="form-control"
                                placeholder="Description"
                                value={description}
                                onChange={(e) => setDescription(e.target.value)}
                            />
                            <button className="btn btn-success" type="submit">Add</button>
                        </div>
                    </form>


                </div>
            </div>

            <div className="row mt-4">
                <div className="col-sm-12">


                    <div className="list-group">
                        {
                            tasks?.map(
                                (item) => (
                                    <div key={item.id} className="list-group-item list-group-item-action">
                                        <h5 className="text-primary">{item.name}</h5>
                                        <h5 className="text-primary">{item.description}</h5>

                                        <div className="d-flex justify-content-between">
                                            <small className="text-muted" >{item.date}</small>
                                            <button onClick={() => closeTask(item.id)} className="btn btn-sm btn-outline-danger">Close</button>
                                        </div>

                                    </div>

                                )

                            )

                        }

                    </div>

                </div>
            </div>
        </div>

    )
}

export default App;