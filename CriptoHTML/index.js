let criptos = []; // Guarda los datos reales obtenidos del backend
function recuperarCriptos() {
  const $ul = document.getElementById('lista_criptomonedas');
  $ul.innerHTML = '';
 resetearFiltros(); // 👈 esto reinicia los filtros visuales


  fetch('https://localhost:7080/api/Cripto/ListarCriptomonedas')
    .then(response => {
      if (!response.ok) throw new Error(`HTTP ${response.status}`);
      return response.json();
    })
    .then(data => {
      criptos = data; // guarda en memoria
      renderizarLista(criptos); // muestra todos
    })
    .catch(error => {
      console.error('Error al consultar la API:', error);
      $ul.innerHTML = '<li>Error al cargar datos</li>';
    });
}
function aplicarFiltros() {
  if (criptos.length === 0) {
    console.warn('No hay datos cargados. Presioná "Actualizar" primero.');
    return;
  }

  const categoria = document.getElementById('filtroCategoria').value;
  const estado = document.getElementById('filtroEstado').value;
  const valorMinimo = parseFloat(document.getElementById('filtroValor').value) || 0;

  const filtrados = criptos.filter(c =>
    (categoria === '' || c.categoria == categoria) &&
    (estado === '' || c.estado == estado) &&
    (c.valorActual >= valorMinimo)
  );

  renderizarLista(filtrados);
}



function renderizarLista(lista) {
  const $ul = document.getElementById('lista_criptomonedas');
  $ul.innerHTML = '';

  if (lista.length === 0) {
    $ul.innerHTML = '<li>No se encontraron criptomonedas con esos filtros</li>';
    return;
  }

  lista.forEach(c => {
    const $li = document.createElement('li');
    $li.classList.add('item-cripto');
    $li.innerHTML = `
      <span class="id">#${c.id}</span>
      <span class="nombre">${c.nombre} (${c.simbolo})</span>
      <span class="valor">$${c.valorActual}</span>
      <span class="fecha">${c.ultimaActualizacion}</span>
      <span class="categoria">${c.categoria}</span>
      <span class="estado">${c.estado}</span>
    `;
    $ul.appendChild($li);
  });
}
function resetearFiltros() {
  document.getElementById('filtroCategoria').value = '';
  document.getElementById('filtroEstado').value = '';
  document.getElementById('filtroValor').value = '';
}


//fetch('https://localhost:7246/api/Aero/Vuelos')

/*

function recuperarCriptos() {
    const $ul = document.getElementById('lista_criptomonedas') // Obtiene el elemento <ul> donde se mostrarán los usuarios

    const prom = fetch('https://localhost:7080/api/Cripto/ListarCriptomonedas') //con la webapi local corriendo en c# hago el fetch a esa url en este caso el localhost es 7080
    const rpta= prom.then((response) => {                                       //ese localhost lo obtengo luego de correr el swagger y ver en que puerto esta corriendo
    
        //obtuve 200 ok del pedido http
        return response.json() // Convierte la respuesta en formato JSON
    })
    rpta.then((data) => {
         criptos = data;// guarda los datos reales 

        console.log(data) //en este caso obtengo la respuesta en consola de lo que trae el boton consultar 
        //obtuve los datos del body del http response
        $ul.innerHTML = '' // Limpia el contenido previo del <ul>
        data .forEach(element => {
            const $li = document.createElement('li') // Crea un nuevo elemento <li>
             $li.classList.add('item-cripto');
              //$li.innerHTML = `#${element.id} | ${element.nombre} | ${element.simbolo} | ${element.valorActual} | ${element.ultimaActualizacion}| ${element.categoria}| ${element.estado}`; // Asigna el contenido al <li>
            $li.innerHTML = `
  <span class="id">#${element.id}</span>
  <span class="nombre">${element.nombre} (${element.simbolo})</span>
  <span class="valor">$${element.valorActual}</span>
   <span class="fecha">${element.ultimaActualizacion}</span>
   <span class="categoria">${element.categoria}</span>
   <span class="estado">${element.estado}</span>
 `;




            $ul.appendChild($li) // Agrega el <li> al <ul>
});
    }).catch((error) => {
        console.error('Error de comunicacion HTTP') // Maneja cualquier error que ocurra durante la solicitud
    }).finally(() => {
        console.log('Bloque ejecutado independientemente del resultado  de la promesa   ') // Indica que el proceso ha finalizado
    })
}

*/


// ejemplo de respuesta en consola obtenida de la webapi local en c# con lo que tenemos que trabajar en el .then(data) para mostrar la lista de criptomonedas
// (6) [{…}, {…}, {…}, {…}, {…}, {…}]
// 0
// : 
// {id: 3, nombre: 'BITCOIN', simbolo: 'BTC', valorActual: 115127.01, ultimaActualizacion: '2025-10-28T12:52:34.347', …}
// 1
// : 
// {id: 4, nombre: 'ETHEREUM', simbolo: 'ETH', valorActual: 616067, ultimaActualizacion: '2025-10-19T00:00:00', …}
// 2
// : 
// {id: 5, nombre: 'TETHER', simbolo: 'USDT', valorActual: 1550, ultimaActualizacion: '2025-10-19T00:00:00', …}
// 3
// : 
// {id: 6, nombre: 'DAI', simbolo: 'DAI', valorActual: 1550, ultimaActualizacion: '2025-10-19T00:00:00', …}
// 4
// : 
// {id: 7, nombre: 'SOLANA', simbolo: 'SOL', valorActual: 295899, ultimaActualizacion: '2025-10-19T00:00:00', …}
// 5
// : 
// {id: 8, nombre: 'POLYGON', simbolo: 'POL', valorActual: 301.08, ultimaActualizacion: '2025-10-19T00:00:00', …}
// length
// : 
// 6




//para comentar en grupo se usa  .JS =  //       /* */
//                               .HTML= <!--   -->

//faltaria ver el tema de la autenticacion que pretende verificar el usuario y validar que ese usuario tenga permisos para ver la lista de usuarios
//y validar que tenga la autorizacion para ver la lista de usuarios

//nosotros usamos JWT (json web token) para manejar la autenticacion y autorizacion
//ese token se envia en el header de la peticion http
// 🧪 Datos simulados (mock)

