export function exampleFunction() {
    alert("Hello Blazor School")
}

window.RenderProgressBar = (count) => {
    const Progress = () => React.createElement(
        Fabric.ProgressIndicator,
        {
            'label': 'React Counter',
            'description': count,
            'percentComplete': (count % 10) * 0.1
        },
        null
    );

    ReactDOM.render(Progress(), document.getElementById('reactProgressBar'));
}