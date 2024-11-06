// Copyright 2021 Yubico AB
//
// Licensed under the Apache License, Version 2.0 (the "License").
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Yubico.Core.Iso7816;

namespace Yubico.YubiKey.Management.Commands
{
    /// <summary>
    /// Gets detailed information about the YubiKey and its current configuration.
    /// </summary>
    /// <remarks>
    /// This class has a corresponding partner class <see cref="GetThalesSerialNumberResponse"/>
    /// </remarks>
    public class GetThalesSerialNumberCommand : IYubiKeyCommand<GetThalesSerialNumberResponse>
    {

        /// <summary>
        /// Gets the YubiKeyApplication to which this command belongs.
        /// </summary>
        /// <value>
        /// <see cref="YubiKeyApplication.Management"/>
        /// </value>
        public YubiKeyApplication Application => YubiKeyApplication.Management;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetThalesSerialNumberCommand"/> class.
        /// </summary>
        public GetThalesSerialNumberCommand()
        {

        }

        /// <inheritdoc />
        public CommandApdu CreateCommandApdu() => new CommandApdu
        {
            Cla = 0x80,
            Ins = 0xCA,
            P1 = 0x01,
            P2 = 0x04,
        };

        /// <inheritdoc />
        public GetThalesSerialNumberResponse CreateResponseForApdu(ResponseApdu responseApdu) =>
            new GetThalesSerialNumberResponse(responseApdu);
    }
}
