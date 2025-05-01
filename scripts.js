function getBaseUrl() {
  const ip = document.getElementById("ip").value;
  return `https://${ip}/api/mouse`;
}

function getBaseUrlKeyboard() {
  const ip = document.getElementById("ip").value;
  return `https://${ip}/api/keyboard`;
}

// Navegación entre vistas
document.querySelectorAll('aside nav button').forEach(btn => {
  btn.addEventListener('click', () => {
    document.querySelectorAll('aside nav button').forEach(b => b.classList.remove('active'));
    btn.classList.add('active');
    document.querySelectorAll('main .view').forEach(v => v.classList.remove('active'));
    document.getElementById(btn.dataset.view).classList.add('active');
  });
});

// Estado de conexión simulado
function setStatus(connected) {
  const statusEl = document.getElementById('status');
  statusEl.textContent = connected ? 'Conectado' : 'Desconectado';
  statusEl.className = connected ? 'status connected' : 'status disconnected';
}
setStatus(false);

// Funciones existentes para mouse y teclado (omitiendo detalles)...
async function clickMouse() {
  const button = document.getElementById("buttonType").value;
  const doubleClick = document.getElementById("doubleClickToggle").checked;
  const res = await fetch(`${getBaseUrl()}/click?button=${button}&doubleClick=${doubleClick}`, {
    method: "POST"
  });
  if (!res.ok) console.error("Error al hacer click:", res.status);
}

async function scrollMouse() {
  const amount = document.getElementById("scrollAmount").value;
  const horizontal = document.getElementById("horizontalScroll").checked;
  await fetch(`${getBaseUrl()}/scroll?amount=${amount}&horizontal=${horizontal}`, {
      method: "POST"
  });
}

async function getPosition() {
  const res = await fetch(`${getBaseUrl()}/position`);
  const data = await res.json();
  document.getElementById("position").innerText = `X: ${data.x}, Y: ${data.y}`;
}

async function moveMouse() {
  await fetch(`${getBaseUrl()}/move`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
          x: parseInt(document.getElementById("x").value),
          y: parseInt(document.getElementById("y").value),
          relative: document.getElementById("relative").checked
      })
  });
}

async function writeText() {
  const text = document.getElementById("textInput").value;
  await fetch(`${getBaseUrlKeyboard()}/write`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ Text: text })
  });
}

async function pressKey() {
  const key = document.getElementById("keySelect").value;
  await fetch(`${getBaseUrlKeyboard()}/press`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ Key: key })
  });
}

// Comandos rápidos
function sendCommand(cmd) {
  const log = document.getElementById('logConsole');
  log.textContent += `> Ejecutando: ${cmd}\n`;
  // Aquí harías fetch a tu API de comandos
}

const touchpad = document.getElementById("touchpad");
        let lastX = 0, lastY = 0;

        touchpad.addEventListener("touchstart", (e) => {
            const touch = e.touches[0];
            lastX = touch.clientX;
            lastY = touch.clientY;
        });

        touchpad.addEventListener("touchmove", (e) => {
            e.preventDefault();



            if (e.touches.length === 1) {

                const sens = parseFloat(document.getElementById("sensitivity").value);
                const touch = e.touches[0];
                let deltaX = touch.clientX - lastX;
                let deltaY = touch.clientY - lastY;
                deltaX = Math.round(deltaX * sens);
                deltaY = Math.round(deltaY * sens);

                lastX = touch.clientX;
                lastY = touch.clientY;

                // Enviamos DeltaX y DeltaY con la misma capitalización que el DTO
                fetch(`${getBaseUrl()}/move-touchpad`, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json", //  Asegura que esto no tenga errores de tipado
                        "Accept": "application/json"        //  Opcional, pero ayuda a clarificar
                    },
                    body: JSON.stringify({
                        deltaX: Math.round(deltaX),  //  Redondea por si acaso
                        deltaY: Math.round(deltaY)
                    })
                }).then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`);
                    }
                    return response.json(); //  Si el backend devuelve un JSON
                }).catch(error => console.error("Error en la petición:", error));
            }
            else if (e.touches.length === 2) {
                // Scroll vertical
                const d0 = e.touches[0], d1 = e.touches[1];
                // Calculá distancia vertical media
                const midY = (d0.clientY + d1.clientY) / 2;
                // tenés que guardar lastMidY, etc…
                const deltaScroll = lastMidY - midY;
                lastMidY = midY;
                fetch(`${getBaseUrl()}/scroll`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        Amount: Math.round(deltaScroll),
                        Horizontal: false
                    })
                });
            }
        });

        function togglePresMode() {
            document.getElementById("touchpad").style.cursor =
                document.getElementById("presentationMode").checked ? "none" : "auto";
        }